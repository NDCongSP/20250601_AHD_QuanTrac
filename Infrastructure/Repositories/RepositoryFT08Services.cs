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

            var fullPath = System.IO.Path.Combine(normalizedPath, model.FileName);

            // Handle possible data URL prefix
            var base64 = model.Base64;
            var commaIdx = base64.IndexOf(",");
            if (commaIdx > -1)
            {
                base64 = base64[(commaIdx + 1)..];
            }

            byte[] bytes = Convert.FromBase64String(base64);
            await System.IO.File.WriteAllBytesAsync(fullPath, bytes);

            var entity = new FT08_FilesManagement
            {
                Id = Guid.NewGuid(),
                PathFile = normalizedPath,  // Use normalized path
                FileName = model.FileName,
                CreateAt = DateTime.Now,
                IsDeleted = false
            };

            dbContext.FT08_FilesManagements.Add(entity);
            await dbContext.SaveChangesAsync();

            return await Result<FT08_FilesManagement>.SuccessAsync(entity, "Uploaded successfully");
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
            try
            {
                EnsureRootFolderExists();

                if (model == null || string.IsNullOrWhiteSpace(model.FolderPath))
                {
                    return await Result<bool>.FailAsync("FolderPath is required");
                }

                // Normalize path
                var normalizedPath = NormalizePath(model.FolderPath);

                if (System.IO.Directory.Exists(normalizedPath))
                {
                    return await Result<bool>.FailAsync("Folder already exists");
                }

                // Create physical folder
                System.IO.Directory.CreateDirectory(normalizedPath);

                // Save to database
                var folderName = System.IO.Path.GetFileName(normalizedPath);
                var entity = new FT08_FilesManagement
                {
                    Id = Guid.NewGuid(),
                    PathFile = normalizedPath,  // Use normalized path
                    FileName = folderName,
                    CreateAt = DateTime.Now,
                    IsDeleted = false
                };

                dbContext.FT08_FilesManagements.Add(entity);
                await dbContext.SaveChangesAsync();

                return await Result<bool>.SuccessAsync(true, "Folder created successfully");
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
            try
            {
                if (model == null || string.IsNullOrWhiteSpace(model.OldPath) || string.IsNullOrWhiteSpace(model.NewName))
                {
                    return await Result<bool>.FailAsync("Invalid parameters");
                }

                // Normalize paths
                var oldPathNormalized = NormalizePath(model.OldPath);
                var directory = System.IO.Path.GetDirectoryName(oldPathNormalized);
                if (string.IsNullOrWhiteSpace(directory))
                {
                    return await Result<bool>.FailAsync("Invalid path");
                }

                var newPath = NormalizePath(System.IO.Path.Combine(directory, model.NewName));

                if (model.IsFolder)
                {
                    if (!System.IO.Directory.Exists(oldPathNormalized))
                    {
                        return await Result<bool>.FailAsync("Folder not found");
                    }
                    
                    // Rename physical folder
                    System.IO.Directory.Move(oldPathNormalized, newPath);

                    // Update folder in database (normalize for comparison)
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
                }
                else
                {
                    if (!System.IO.File.Exists(oldPathNormalized))
                    {
                        return await Result<bool>.FailAsync("File not found");
                    }
                    
                    // Rename physical file
                    System.IO.File.Move(oldPathNormalized, newPath);

                    // Update database - Files store PathFile as parent directory
                    var allItems = dbContext.FT08_FilesManagements.ToList();
                    var oldFileName = System.IO.Path.GetFileName(oldPathNormalized);
                    var entity = allItems.FirstOrDefault(x => 
                        NormalizePath(x.PathFile) == directory && 
                        x.FileName == oldFileName);
                    
                    if (entity != null)
                    {
                        entity.FileName = model.NewName;
                        entity.UpdateAt = DateTime.Now;
                        await dbContext.SaveChangesAsync();
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
                if (!System.IO.Directory.Exists(normalizedPath))
                {
                    return await Result<bool>.FailAsync("Folder not found");
                }
                
                // Delete physical folder
                System.IO.Directory.Delete(normalizedPath, true);

                // Delete folder from database (normalize for comparison)
                var allItems = dbContext.FT08_FilesManagements.ToList();
                var folderEntity = allItems.FirstOrDefault(x => 
                    NormalizePath(x.PathFile) == normalizedPath);
                
                if (folderEntity != null)
                {
                    dbContext.FT08_FilesManagements.Remove(folderEntity);
                }

                // Delete all subfolders and files in this folder from database
                var itemsInFolder = allItems.Where(x => 
                    NormalizePath(x.PathFile).StartsWith(normalizedPath + "\\")).ToList();
                
                if (itemsInFolder.Any())
                {
                    dbContext.FT08_FilesManagements.RemoveRange(itemsInFolder);
                }

                await dbContext.SaveChangesAsync();
            }
            else
            {
                if (!System.IO.File.Exists(normalizedPath))
                {
                    return await Result<bool>.FailAsync("File not found");
                }
                
                // Delete physical file
                System.IO.File.Delete(normalizedPath);

                // Delete from database - Files store PathFile as parent directory
                var directory = System.IO.Path.GetDirectoryName(normalizedPath);
                var fileName = System.IO.Path.GetFileName(normalizedPath);
                var allItems = dbContext.FT08_FilesManagements.ToList();
                var entity = allItems.FirstOrDefault(x => 
                    NormalizePath(x.PathFile) == directory && 
                    x.FileName == fileName);
                
                if (entity != null)
                {
                    dbContext.FT08_FilesManagements.Remove(entity);
                    await dbContext.SaveChangesAsync();
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
