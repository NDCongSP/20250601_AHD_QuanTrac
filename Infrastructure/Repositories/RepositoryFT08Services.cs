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

            fullPath = System.IO.Path.Combine(normalizedPath, model.FileName);

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
                    FileName = model.FileName,
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

                // Normalize path
                normalizedPath = NormalizePath(model.FolderPath);

                if (System.IO.Directory.Exists(normalizedPath))
                {
                    return await Result<bool>.FailAsync("Folder already exists");
                }

                // 1. Create physical folder first
                System.IO.Directory.CreateDirectory(normalizedPath);

                // 2. Save to database with transaction
                await using var transaction = await dbContext.Database.BeginTransactionAsync();
                try
                {
                    var folderName = System.IO.Path.GetFileName(normalizedPath);
                    var entity = new FT08_FilesManagement
                    {
                        Id = Guid.NewGuid(),
                        PathFile = normalizedPath,
                        FileName = folderName,
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

                newPath = NormalizePath(System.IO.Path.Combine(directory, model.NewName));

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
                        var folderEntity = allItems.FirstOrDefault(x => 
                            NormalizePath(x.PathFile) == oldPathNormalized);
                        
                        if (folderEntity != null)
                        {
                            folderEntity.PathFile = newPath;
                            folderEntity.FileName = model.NewName;
                            folderEntity.UpdateAt = DateTime.Now;
                        }

                        // Update all subfolders and files in this folder
                        var itemsInFolder = allItems.Where(x => 
                            NormalizePath(x.PathFile).StartsWith(oldPathNormalized + "\\")).ToList();
                        
                        foreach (var item in itemsInFolder)
                        {
                            var normalizedItemPath = NormalizePath(item.PathFile);
                            var relativePath = normalizedItemPath.Substring(oldPathNormalized.Length);
                            item.PathFile = newPath + relativePath;
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
                    
                    // Validate and fix file extension for PDF files
                    var oldFileName = System.IO.Path.GetFileName(oldPathNormalized);
                    var oldExtension = System.IO.Path.GetExtension(oldFileName);
                    var validatedNewName = model.NewName;
                    
                    // If old file is .pdf, ensure new name keeps .pdf extension
                    if (oldExtension.Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                    {
                        var newExtension = System.IO.Path.GetExtension(validatedNewName);
                        
                        if (string.IsNullOrEmpty(newExtension))
                        {
                            // No extension provided, add .pdf
                            validatedNewName = validatedNewName + ".pdf";
                        }
                        else if (!newExtension.Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                        {
                            // Wrong extension provided, replace with .pdf
                            validatedNewName = System.IO.Path.GetFileNameWithoutExtension(validatedNewName) + ".pdf";
                        }
                        // else: already has .pdf, keep as is
                    }
                    
                    // Recalculate newPath with validated name
                    newPath = NormalizePath(System.IO.Path.Combine(directory, validatedNewName));
                    
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
                            entity.FileName = validatedNewName;
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
                    
                    // Delete all items related to this folder:
                    // 1. Folder itself: PathFile == normalizedPath (e.g., "E:\SCADA\UploadFiles\Tram A")
                    // 2. Files in this folder: PathFile == normalizedPath (e.g., PathFile="E:\SCADA\UploadFiles\Tram A", FileName="file.pdf")
                    // 3. Subfolders: PathFile starts with normalizedPath + "\" (e.g., "E:\SCADA\UploadFiles\Tram A\Subfolder1")
                    // 4. Files in subfolders: PathFile starts with normalizedPath + "\" (e.g., PathFile="E:\SCADA\UploadFiles\Tram A\Subfolder1")
                    var itemsToDelete = allItems.Where(x => 
                    {
                        var itemPath = NormalizePath(x.PathFile);
                        // Match exact path OR any path starting with folder path + "\"
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
            EnsureRootFolderExists();

            // Get all items from database
            var allItems = await Task.FromResult(
                dbContext.FT08_FilesManagements
                    .Where(x => x.IsDeleted == false || x.IsDeleted == null)
                    .OrderBy(x => x.PathFile)
                    .ThenBy(x => x.FileName)
                    .ToList());

            // If no data, create default "New folder"
            if (!allItems.Any())
            {
                var newFolderPath = System.IO.Path.Combine(RootFolder, "New folder");
                
                // Create physical folder
                if (!System.IO.Directory.Exists(newFolderPath))
                {
                    System.IO.Directory.CreateDirectory(newFolderPath);
                }

                var newFolder = new FT08_FilesManagement
                {
                    Id = Guid.NewGuid(),
                    PathFile = newFolderPath,
                    FileName = "New folder",
                    CreateAt = DateTime.Now,
                    IsDeleted = false
                };

                dbContext.FT08_FilesManagements.Add(newFolder);
                await dbContext.SaveChangesAsync();
                
                allItems.Add(newFolder);
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
        var folders = items.Where(x => string.IsNullOrEmpty(System.IO.Path.GetExtension(x.FileName))).ToList();
        var files = items.Where(x => !string.IsNullOrEmpty(System.IO.Path.GetExtension(x.FileName))).ToList();

        // Normalize all paths to use backslash
        foreach (var folder in folders)
        {
            folder.PathFile = NormalizePath(folder.PathFile);
        }
        foreach (var file in files)
        {
            file.PathFile = NormalizePath(file.PathFile);
        }

        // Sort folders by path depth (parents first)
        folders = folders.OrderBy(f => f.PathFile.Split('\\', StringSplitOptions.RemoveEmptyEntries).Length).ToList();

        // Build all folder items first
        foreach (var folder in folders)
        {
            var folderItem = new FolderTreeItem
            {
                Name = folder.FileName,
                FullPath = folder.PathFile,
                IsFolder = true,
                Children = new List<FolderTreeItem>()
            };
            folderMap[folder.PathFile] = folderItem;
        }

        // Build folder hierarchy
        foreach (var folder in folders)
        {
            var folderPath = folder.PathFile;
            var folderItem = folderMap[folderPath];

            // Find parent folder - parent path is the directory containing this folder
            var parentPath = System.IO.Path.GetDirectoryName(folderPath);
            
            if (!string.IsNullOrEmpty(parentPath))
            {
                // Normalize parent path too
                parentPath = NormalizePath(parentPath);
                
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
            else
            {
                // No parent - this is root level
                tree.Add(folderItem);
            }
        }

        // Add files to their parent folders
        foreach (var file in files)
        {
            var fileItem = new FolderTreeItem
            {
                Name = file.FileName,
                FullPath = System.IO.Path.Combine(file.PathFile, file.FileName),
                IsFolder = false,
                Children = new List<FolderTreeItem>()
            };

            if (folderMap.ContainsKey(file.PathFile))
            {
                folderMap[file.PathFile].Children.Add(fileItem);
            }
            else
            {
                // File at root level
                tree.Add(fileItem);
            }
        }

        // Sort children: by type (folders first), then by name (A-Z)
        foreach (var item in folderMap.Values)
        {
            item.Children = item.Children
                .OrderBy(x => x.IsFolder ? 0 : 1)  // Type: Folders (0) first, Files (1) last
                .ThenBy(x => x.Name, StringComparer.CurrentCultureIgnoreCase)  // Name: A-Z (culture-aware, supports Vietnamese)
                .ToList();
        }

        // Sort root level items: by type (folders first), then by name (A-Z)
        return tree
            .OrderBy(x => x.IsFolder ? 0 : 1)  // Type: Folders (0) first, Files (1) last
            .ThenBy(x => x.Name, StringComparer.CurrentCultureIgnoreCase)  // Name: A-Z (culture-aware, supports Vietnamese)
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
}
