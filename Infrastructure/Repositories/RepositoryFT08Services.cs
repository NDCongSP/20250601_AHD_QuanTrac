using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Extentions;
using Application.Services;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RestEase;

namespace Infrastructure.Repositories;

public class RepositoryFT08Services(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor) : IFT08
{
    private const string RootFolder = @"E:\SCADA\UploadFiles";

    private static void EnsureRootFolderExists()
    {
        if (!System.IO.Directory.Exists(RootFolder))
        {
            System.IO.Directory.CreateDirectory(RootFolder);
        }
    }
    public Task<Result<List<FT08_FilesManagement>>> AddRangeAsync([Body] List<FT08_FilesManagement> model)
    {
        throw new NotImplementedException();
    }

    public Task<Result<FT08_FilesManagement>> DeleteAsync([Body] FT08_FilesManagement model)
    {
        throw new NotImplementedException();
    }

    public Task<Result<FT08_FilesManagement>> DeleteRangeAsync([Body] List<FT08_FilesManagement> model)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<List<FT08_FilesManagement>>> GetAllAsync()
    {
        try
        {
            var data = await Task.FromResult(
                dbContext.FT08_FilesManagements
                    .OrderBy(x => x.PathFile)
                    .ThenBy(x => x.FileName)
                    .ToList());
            return await Result<List<FT08_FilesManagement>>.SuccessAsync(data, "Successful");
        }
        catch (Exception ex)
        {
            return await Result<List<FT08_FilesManagement>>.FailAsync(new List<string> { ex.Message });
        }
    }

    public Task<Result<FT08_FilesManagement>> GetByIdAsync([Path] Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<string>> GetPdfAsBase64Async([Query] string pathFile)
    {
        try
        {
            if (string.IsNullOrEmpty(pathFile) || !System.IO.File.Exists(pathFile))
            {
                var err = new ErrorResponse();
                err.Errors.Add("Warning", "File not found or path is invalid.");
                return await Result<string>.FailAsync(JsonConvert.SerializeObject(err));
            }

            // Đọc file PDF
            byte[] pdfBytes = await System.IO.File.ReadAllBytesAsync(pathFile);

            // Chuyển thành Base64
            string base64String = Convert.ToBase64String(pdfBytes);

            // Trả về kết quả
            return await Result<string>.SuccessAsync(base64String, "Successful");
        }
        catch (Exception ex)
        {
            var err = new ErrorResponse();
            err.Errors.Add("Error", $"{ex.Message} | {ex.InnerException}");
            return await Result<string>.FailAsync(JsonConvert.SerializeObject(err));
        }
    }

    public Task<Result<FT08_FilesManagement>> InsertAsync([Body] FT08_FilesManagement model)
    {
        throw new NotImplementedException();
    }

    public Task<Result<FT08_FilesManagement>> UpdateAsync([Body] FT08_FilesManagement model)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<FT08_FilesManagement>> UploadPdfFileAsync([Body] UploadPdfFileRequest model)
    {
        string? fullPath = null;
        try
        {
            EnsureRootFolderExists();

            if (model == null)
            {
                return await Result<FT08_FilesManagement>.FailAsync("Request is null");
            }
            if (string.IsNullOrWhiteSpace(model.PathFile))
            {
                return await Result<FT08_FilesManagement>.FailAsync("PathFile is required");
            }
            if (string.IsNullOrWhiteSpace(model.FileName))
            {
                return await Result<FT08_FilesManagement>.FailAsync("FileName is required");
            }
            if (string.IsNullOrWhiteSpace(model.Base64))
            {
                return await Result<FT08_FilesManagement>.FailAsync("Base64 content is required");
            }

            // Normalize path
            var normalizedPath = NormalizePath(model.PathFile);

            // Ensure directory exists
            if (!System.IO.Directory.Exists(normalizedPath))
            {
                System.IO.Directory.CreateDirectory(normalizedPath);
            }

            // Get unique file name (adds (1), (2), etc. if duplicate)
            var uniqueFileName = GetUniqueFileName(normalizedPath, model.FileName);
            fullPath = System.IO.Path.Combine(normalizedPath, uniqueFileName);

            // Handle possible data URL prefix
            var base64 = model.Base64;
            var commaIdx = base64.IndexOf(",");
            if (commaIdx > -1)
            {
                base64 = base64[(commaIdx + 1)..];
            }

            // 1. Create physical file first
            byte[] bytes = Convert.FromBase64String(base64);
            await System.IO.File.WriteAllBytesAsync(fullPath, bytes);

            // 2. Save to database with transaction
            await using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                var entity = new FT08_FilesManagement
                {
                    Id = Guid.NewGuid(),
                    PathFile = normalizedPath,
                    FileName = uniqueFileName,  // Use unique file name
                    CreateAt = DateTime.Now,
                    IsDeleted = false
                };

                dbContext.FT08_FilesManagements.Add(entity);
                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return await Result<FT08_FilesManagement>.SuccessAsync(entity, "Uploaded successfully");
            }
            catch
            {
                await transaction.RollbackAsync();
                // Rollback: Delete physical file if DB save failed
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                throw;
            }
        }
        catch (Exception ex)
        {
            var err = new ErrorResponse();
            err.Errors.Add("Error", $"{ex.Message} | {ex.InnerException}");
            return await Result<FT08_FilesManagement>.FailAsync(JsonConvert.SerializeObject(err));
        }
    }

    public async Task<Result<bool>> CreateFolderAsync([Body] CreateFolderRequest model)
    {
        string? normalizedPath = null;
        try
        {
            EnsureRootFolderExists();

            if (model == null || string.IsNullOrWhiteSpace(model.FolderPath))
            {
                return await Result<bool>.FailAsync("FolderPath is required");
            }

            // Normalize path and get unique folder name if duplicate exists
            var requestedPath = NormalizePath(model.FolderPath);
            var parentPath = System.IO.Path.GetDirectoryName(requestedPath);
            var requestedName = System.IO.Path.GetFileName(requestedPath);

            if (string.IsNullOrWhiteSpace(parentPath) || string.IsNullOrWhiteSpace(requestedName))
            {
                return await Result<bool>.FailAsync("Invalid folder path");
            }

            // Get unique name (adds (1), (2), etc. if duplicate)
            var uniqueName = GetUniqueFolderName(parentPath, requestedName);
            normalizedPath = System.IO.Path.Combine(parentPath, uniqueName);

            // 1. Create physical folder first
            System.IO.Directory.CreateDirectory(normalizedPath);

            // 2. Save to database with transaction
            await using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                // Save folder as a single path entry (FileName empty, PathFile = full folder path)
                var entity = new FT08_FilesManagement
                {
                    Id = Guid.NewGuid(),
                    PathFile = NormalizePath(normalizedPath), // Full folder path
                    FileName = string.Empty,                   // Folder name is implied by last segment of PathFile
                    CreateAt = DateTime.Now,
                    IsDeleted = false
                };

                dbContext.FT08_FilesManagements.Add(entity);
                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return await Result<bool>.SuccessAsync(true, "Folder created successfully");
            }
            catch
            {
                await transaction.RollbackAsync();
                // Rollback: Delete physical folder if DB save failed
                if (System.IO.Directory.Exists(normalizedPath))
                {
                    System.IO.Directory.Delete(normalizedPath, true);
                }
                throw;
            }
        }
        catch (Exception ex)
        {
            var err = new ErrorResponse();
            err.Errors.Add("Error", $"{ex.Message} | {ex.InnerException}");
            return await Result<bool>.FailAsync(JsonConvert.SerializeObject(err));
        }
    }

    public async Task<Result<bool>> RenameItemAsync([Body] RenameItemRequest model)
    {
        string? oldPathNormalized = null;
        string? newPath = null;
        try
        {
            if (model == null || string.IsNullOrWhiteSpace(model.OldPath) || string.IsNullOrWhiteSpace(model.NewName))
            {
                return await Result<bool>.FailAsync("Invalid parameters");
            }

            // Normalize paths
            oldPathNormalized = NormalizePath(model.OldPath);
            var directory = System.IO.Path.GetDirectoryName(oldPathNormalized);
            if (string.IsNullOrWhiteSpace(directory))
            {
                return await Result<bool>.FailAsync("Invalid path");
            }

            // For files, validate PDF extension first
            var requestedName = model.NewName;
            if (!model.IsFolder)
            {
                var oldFileName = System.IO.Path.GetFileName(oldPathNormalized);
                var oldExtension = System.IO.Path.GetExtension(oldFileName);

                // If old file is .pdf, ensure new name keeps .pdf extension
                if (oldExtension.Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    var newExtension = System.IO.Path.GetExtension(requestedName);

                    if (string.IsNullOrEmpty(newExtension))
                    {
                        // No extension provided, add .pdf
                        requestedName = requestedName + ".pdf";
                    }
                    else if (!newExtension.Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                    {
                        // Wrong extension provided, replace with .pdf
                        requestedName = System.IO.Path.GetFileNameWithoutExtension(requestedName) + ".pdf";
                    }
                    // else: already has .pdf, keep as is
                }
            }

            // Get unique name (adds (1), (2), etc. if duplicate)
            string finalName;
            if (model.IsFolder)
            {
                finalName = GetUniqueFolderName(directory, requestedName);
            }
            else
            {
                finalName = GetUniqueFileName(directory, requestedName);
            }

            newPath = NormalizePath(System.IO.Path.Combine(directory, finalName));

            if (model.IsFolder)
            {
                if (!System.IO.Directory.Exists(oldPathNormalized))
                {
                    return await Result<bool>.FailAsync("Folder not found");
                }

                // 1. Rename physical folder first
                System.IO.Directory.Move(oldPathNormalized, newPath);

                // 2. Update database with transaction
                await using var transaction = await dbContext.Database.BeginTransactionAsync();
                try
                {
                    var allItems = dbContext.FT08_FilesManagements.ToList();
                    var parentPath = NormalizePath(System.IO.Path.GetDirectoryName(oldPathNormalized) ?? "");
                    var oldFolderName = System.IO.Path.GetFileName(oldPathNormalized);

                    // Find the folder entity (new representation only)
                    var folderEntity = allItems.FirstOrDefault(x =>
                        NormalizePath(x.PathFile) == oldPathNormalized && string.IsNullOrEmpty(x.FileName));

                    if (folderEntity != null)
                    {
                        // New representation: set PathFile to new path and keep FileName empty
                        folderEntity.PathFile = newPath;
                        folderEntity.FileName = string.Empty;
                        folderEntity.UpdateAt = DateTime.Now;
                    }

                    // Update PathFile for all items inside this folder (direct children and nested)
                    // Their PathFile needs to change from oldPathNormalized to newPath
                    var itemsInFolder = allItems.Where(x =>
                    {
                        var itemPath = NormalizePath(x.PathFile);
                        return itemPath == oldPathNormalized ||
                               itemPath.StartsWith(oldPathNormalized + "\\");
                    }).ToList();

                    foreach (var item in itemsInFolder)
                    {
                        var normalizedItemPath = NormalizePath(item.PathFile);
                        if (normalizedItemPath == oldPathNormalized)
                        {
                            // Direct child: PathFile is exactly the old folder path
                            item.PathFile = newPath;
                        }
                        else
                        {
                            // Nested child: PathFile starts with old folder path
                            var relativePath = normalizedItemPath.Substring(oldPathNormalized.Length);
                            item.PathFile = newPath + relativePath;
                        }
                        item.UpdateAt = DateTime.Now;
                    }

                    await dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    // Rollback: Rename folder back to original name
                    if (System.IO.Directory.Exists(newPath))
                    {
                        System.IO.Directory.Move(newPath, oldPathNormalized);
                    }
                    throw;
                }
            }
            else
            {
                if (!System.IO.File.Exists(oldPathNormalized))
                {
                    return await Result<bool>.FailAsync("File not found");
                }

                var oldFileName = System.IO.Path.GetFileName(oldPathNormalized);

                // 1. Rename physical file first
                System.IO.File.Move(oldPathNormalized, newPath);

                // 2. Update database with transaction
                await using var transaction = await dbContext.Database.BeginTransactionAsync();
                try
                {
                    var allItems = dbContext.FT08_FilesManagements.ToList();
                    var entity = allItems.FirstOrDefault(x =>
                        NormalizePath(x.PathFile) == directory &&
                        x.FileName == oldFileName);

                    if (entity != null)
                    {
                        entity.FileName = finalName;  // Use unique name
                        entity.UpdateAt = DateTime.Now;
                    }

                    await dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    // Rollback: Rename file back to original name
                    if (System.IO.File.Exists(newPath))
                    {
                        System.IO.File.Move(newPath, oldPathNormalized);
                    }
                    throw;
                }
            }

            return await Result<bool>.SuccessAsync(true, "Renamed successfully");
        }
        catch (Exception ex)
        {
            var err = new ErrorResponse();
            err.Errors.Add("Error", $"{ex.Message} | {ex.InnerException}");
            return await Result<bool>.FailAsync(JsonConvert.SerializeObject(err));
        }
    }

    public async Task<Result<bool>> DeleteItemAsync([Body] DeleteItemRequest model)
    {
        try
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Path))
            {
                return await Result<bool>.FailAsync("Path is required");
            }

            // Normalize path
            var normalizedPath = NormalizePath(model.Path);

            // Prevent deleting root folder (UploadFiles)
            var normalizedRootFolder = NormalizePath(RootFolder);
            if (normalizedPath.Equals(normalizedRootFolder, StringComparison.OrdinalIgnoreCase))
            {
                return await Result<bool>.FailAsync("Cannot delete root folder");
            }

            if (model.IsFolder)
            {
                // 1. Delete physical folder first (if exists)
                var folderExists = System.IO.Directory.Exists(normalizedPath);
                if (folderExists)
                {
                    System.IO.Directory.Delete(normalizedPath, true);
                }

                // 2. Delete from database with transaction (always delete from DB even if folder doesn't exist)
                await using var transaction = await dbContext.Database.BeginTransactionAsync();
                try
                {
                    var allItems = dbContext.FT08_FilesManagements.ToList();
                    var parentPath = NormalizePath(System.IO.Path.GetDirectoryName(normalizedPath) ?? "");
                    var folderName = System.IO.Path.GetFileName(normalizedPath);

                    // Delete all items related to this folder (new representation only for folder entity):
                    // - Folder itself (PathFile == normalizedPath && FileName == "")
                    // - Direct children: PathFile == folder full path
                    // - Nested children: PathFile starts with folder full path + "\\"
                    var itemsToDelete = allItems.Where(x =>
                    {
                        var itemPath = NormalizePath(x.PathFile);

                        // Folder entity (new)
                        if (itemPath == normalizedPath && string.IsNullOrEmpty(x.FileName))
                        {
                            return true;
                        }

                        // Items inside this folder (direct or nested)
                        return itemPath == normalizedPath ||
                               itemPath.StartsWith(normalizedPath + "\\");
                    }).ToList();

                    if (itemsToDelete.Any())
                    {
                        dbContext.FT08_FilesManagements.RemoveRange(itemsToDelete);
                    }

                    await dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    // If DB delete failed and we deleted the physical folder, we can't rollback the folder deletion
                    // This is acceptable as the folder is already gone
                    throw;
                }
            }
            else
            {
                // 1. Delete physical file first (if exists)
                var fileExists = System.IO.File.Exists(normalizedPath);
                if (fileExists)
                {
                    System.IO.File.Delete(normalizedPath);
                }

                // 2. Delete from database with transaction (always delete from DB even if file doesn't exist)
                await using var transaction = await dbContext.Database.BeginTransactionAsync();
                try
                {
                    var directory = System.IO.Path.GetDirectoryName(normalizedPath);
                    var fileName = System.IO.Path.GetFileName(normalizedPath);
                    var allItems = dbContext.FT08_FilesManagements.ToList();
                    var entity = allItems.FirstOrDefault(x =>
                        NormalizePath(x.PathFile) == directory &&
                        x.FileName == fileName);

                    if (entity != null)
                    {
                        dbContext.FT08_FilesManagements.Remove(entity);
                    }

                    await dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    // If DB delete failed and we deleted the physical file, we can't rollback the file deletion
                    // This is acceptable as the file is already gone
                    throw;
                }
            }

            return await Result<bool>.SuccessAsync(true, "Deleted successfully");
        }
        catch (Exception ex)
        {
            var err = new ErrorResponse();
            err.Errors.Add("Error", $"{ex.Message} | {ex.InnerException}");
            return await Result<bool>.FailAsync(JsonConvert.SerializeObject(err));
        }
    }

    public async Task<Result<List<FolderTreeItem>>> GetFolderTreeAsync()
    {
        try
        {
            // Ensure root folder exists physically
            EnsureRootFolderExists();

            // Get all items from database
            var allItems = await Task.FromResult(
                dbContext.FT08_FilesManagements
                    .Where(x => x.IsDeleted == false || x.IsDeleted == null)
                    .OrderBy(x => x.PathFile)
                    .ThenBy(x => x.FileName)
                    .ToList());

            // Ensure root folder entry exists in DB
            var normalizedRootFolder = NormalizePath(RootFolder);
            var rootParentPath = NormalizePath(System.IO.Path.GetDirectoryName(RootFolder) ?? "E:\\SCADA");
            var rootFolderName = System.IO.Path.GetFileName(RootFolder); // "UploadFiles"

            var rootEntry = allItems.FirstOrDefault(x =>
                NormalizePath(x.PathFile) == normalizedRootFolder && string.IsNullOrEmpty(x.FileName));

            if (rootEntry == null)
            {
                // Create root folder entry in DB (new representation)
                rootEntry = new FT08_FilesManagement
                {
                    Id = Guid.NewGuid(),
                    PathFile = normalizedRootFolder, // Full root path
                    FileName = string.Empty,          // Name implied by PathFile
                    CreateAt = DateTime.Now,
                    IsDeleted = false
                };

                dbContext.FT08_FilesManagements.Add(rootEntry);
                await dbContext.SaveChangesAsync();

                allItems.Add(rootEntry);
            }

            var tree = BuildFolderTreeFromDatabase(allItems);
            return await Result<List<FolderTreeItem>>.SuccessAsync(tree, "Successful");
        }
        catch (Exception ex)
        {
            var err = new ErrorResponse();
            err.Errors.Add("Error", $"{ex.Message} | {ex.InnerException}");
            return await Result<List<FolderTreeItem>>.FailAsync(JsonConvert.SerializeObject(err));
        }
    }

    private List<FolderTreeItem> BuildFolderTreeFromDatabase(List<FT08_FilesManagement> items)
    {
        var tree = new List<FolderTreeItem>();
        var folderMap = new Dictionary<string, FolderTreeItem>(StringComparer.OrdinalIgnoreCase);

        // Separate folders and files
        // New convention: folder records have FileName empty; files have FileName set
        var folders = items.Where(x => string.IsNullOrEmpty(x.FileName)).ToList();
        var files = items.Where(x => !string.IsNullOrEmpty(x.FileName)).ToList();

        // Normalize all paths to use backslash
        foreach (var folder in folders)
        {
            folder.PathFile = NormalizePath(folder.PathFile);
        }
        foreach (var file in files)
        {
            file.PathFile = NormalizePath(file.PathFile);
        }

        // Build folder full paths and sort by depth (parents first)
        var foldersWithFullPath = folders.Select(f => new
        {
            Entity = f,
            FullPath = string.IsNullOrEmpty(f.FileName)
                ? NormalizePath(f.PathFile)
                : NormalizePath(System.IO.Path.Combine(f.PathFile, f.FileName))
        }).OrderBy(f => f.FullPath.Split('\\', StringSplitOptions.RemoveEmptyEntries).Length)
        .ToList();

        // Build all folder items first
        foreach (var folderData in foldersWithFullPath)
        {
            var folderName = !string.IsNullOrEmpty(folderData.Entity.FileName)
                ? folderData.Entity.FileName
                : System.IO.Path.GetFileName(folderData.FullPath);
            var folderItem = new FolderTreeItem
            {
                Name = folderName,
                FullPath = folderData.FullPath,
                IsFolder = true,
                CreatedAt = folderData.Entity.CreateAt,
                Children = new List<FolderTreeItem>()
            };
            folderMap[folderData.FullPath] = folderItem;
        }

        // Build folder hierarchy
        foreach (var folderData in foldersWithFullPath)
        {
            var folderFullPath = folderData.FullPath;
            var folderItem = folderMap[folderFullPath];

            // For new representation, parent path is the directory of FullPath
            var parentPath = NormalizePath(System.IO.Path.GetDirectoryName(folderFullPath) ?? "");

            if (folderMap.ContainsKey(parentPath))
            {
                // Add to parent folder
                folderMap[parentPath].Children.Add(folderItem);
            }
            else
            {
                // Parent not found in map - add to root level
                tree.Add(folderItem);
            }
        }

        // Add files to their parent folders
        foreach (var file in files)
        {
            var fileItem = new FolderTreeItem
            {
                Name = file.FileName,
                FullPath = NormalizePath(System.IO.Path.Combine(file.PathFile, file.FileName)),
                IsFolder = false,
                CreatedAt = file.CreateAt,
                Children = new List<FolderTreeItem>()
            };

            var parentFolderPath = NormalizePath(file.PathFile);
            if (folderMap.ContainsKey(parentFolderPath))
            {
                folderMap[parentFolderPath].Children.Add(fileItem);
            }
            else
            {
                // File at root level
                tree.Add(fileItem);
            }
        }

        // Sort children: by type (folders first), then by CreatedAt (newest first), then by name (A-Z)
        foreach (var item in folderMap.Values)
        {
            item.Children = item.Children
                .OrderBy(x => x.IsFolder ? 0 : 1) // Folders first, then files
                .ThenByDescending(x => x.CreatedAt) // Newest to oldest
                .ThenBy(x => x.Name, StringComparer.CurrentCultureIgnoreCase) // Name A-Z
                .ToList();
        }

        // Sort root level items: Folders first, then files; CreatedAt newest-to-oldest; Name A-Z
        return tree
            .OrderBy(x => x.IsFolder ? 0 : 1)
            .ThenByDescending(x => x.CreatedAt)
            .ThenBy(x => x.Name, StringComparer.CurrentCultureIgnoreCase)
            .ToList();
    }

    private string GetRelativePath(string fullPath)
    {
        if (string.IsNullOrEmpty(fullPath))
            return string.Empty;

        if (fullPath.StartsWith(RootFolder, StringComparison.OrdinalIgnoreCase))
        {
            var relative = fullPath.Substring(RootFolder.Length).TrimStart('\\', '/');
            return relative;
        }

        return fullPath;
    }

    private string NormalizePath(string path)
    {
        if (string.IsNullOrEmpty(path))
            return path;

        // Replace forward slashes with backslashes and remove trailing slashes
        return path.Replace('/', '\\').TrimEnd('\\');
    }

    /// <summary>
    /// Generate unique folder name by appending (1), (2), etc. if name already exists
    /// </summary>
    private string GetUniqueFolderName(string parentPath, string baseName)
    {
        var normalizedParentPath = NormalizePath(parentPath);
        var candidatePath = System.IO.Path.Combine(normalizedParentPath, baseName);

        if (!System.IO.Directory.Exists(candidatePath))
        {
            return baseName;
        }

        int counter = 1;
        string newName;
        do
        {
            newName = $"{baseName} ({counter})";
            candidatePath = System.IO.Path.Combine(normalizedParentPath, newName);
            counter++;
        } while (System.IO.Directory.Exists(candidatePath));

        return newName;
    }

    /// <summary>
    /// Generate unique file name by appending (1), (2), etc. before extension if name already exists
    /// </summary>
    private string GetUniqueFileName(string parentPath, string baseFileName)
    {
        var normalizedParentPath = NormalizePath(parentPath);
        var candidatePath = System.IO.Path.Combine(normalizedParentPath, baseFileName);

        if (!System.IO.File.Exists(candidatePath))
        {
            return baseFileName;
        }

        var nameWithoutExt = System.IO.Path.GetFileNameWithoutExtension(baseFileName);
        var extension = System.IO.Path.GetExtension(baseFileName);

        int counter = 1;
        string newName;
        do
        {
            newName = $"{nameWithoutExt} ({counter}){extension}";
            candidatePath = System.IO.Path.Combine(normalizedParentPath, newName);
            counter++;
        } while (System.IO.File.Exists(candidatePath));

        return newName;
    }
}
