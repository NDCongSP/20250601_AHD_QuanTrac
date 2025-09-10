//using Application.DTOs.Response;
//using Application.Extentions;
//using Application.Services;
//using Domain;
//using Domain.NotTable;
//using Microsoft.AspNetCore.Components;
//using Microsoft.Extensions.Localization;
//using MudBlazor;
//using System.Text.Json;

//namespace UI.Pages.SettingManagement
//{
//    public partial class Index
//    {
//        [Inject]
//        private IStringLocalizer<Index> Localizer { get; set; } = null!;

//        [Inject]
//        private IFT01 _ft01Service { get; set; } = null!;

//        [Inject]
//        private ISnackbar _snackbar { get; set; } = null!;

//        private ConfigModel _config = new();
//        private bool _isLoading = true;
//        private bool _isSaving = false;

//        protected override async Task OnInitializedAsync()
//        {
//            await base.OnInitializedAsync();
//            await LoadConfigAsync();
//        }

//        private async Task LoadConfigAsync()
//        {
//            try
//            {
//                _isLoading = true;
//                StateHasChanged();

//                var result = await _ft01Service.GetConfigAsync();
//                if (result.Succeeded && result.Data != null)
//                {
//                    _config = result.Data;
//                }
//            }
//            catch (Exception ex)
//            {
//                _snackbar.Add($"Error loading settings: {ex.Message}", Severity.Error);
//            }
//            finally
//            {
//                _isLoading = false;
//                StateHasChanged();
//            }
//        }

//        private async Task HandleValidSubmit()
//        {
//            try
//            {
//                _isSaving = true;
//                StateHasChanged();

//                var result = await _ft01Service.UpdateConfigAsync(_config);
//                if (result.Succeeded)
//                {
//                    _snackbar.Add("Settings saved successfully!", Severity.Success);
//                }
//                else
//                {
//                    _snackbar.Add($"Error saving settings: {string.Join(", ", result.Messages)}", Severity.Error);
//                }
//            }
//            catch (Exception ex)
//            {
//                _snackbar.Add($"Error saving settings: {ex.Message}", Severity.Error);
//            }
//            finally
//            {
//                _isSaving = false;
//                StateHasChanged();
//            }
//        }
//    }
//}
