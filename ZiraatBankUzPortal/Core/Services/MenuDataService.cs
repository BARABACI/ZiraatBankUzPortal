using Dapper.Oracle;
using System.Data;
using ZiraatBankUzPortal.Shared.DataAccess;
using ZiraatBankUzPortal.Shared.DisplayModel;
using ZiraatBankUzPortal.Shared.Dto;
using ZiraatBankUzPortal.Shared.Helper;
using ZiraatBankUzPortal.Shared.Model;

namespace ZiraatBankUzPortal.Core.Services
{
    public class MenuDataService : IMenuDataService
    {
        private readonly IOracleDataAccess _orcaleDataAccess;
        private readonly IConfiguration _config;
        public MenuDataService(IOracleDataAccess orcaleDataAccess, IConfiguration config)
        {
            _orcaleDataAccess = orcaleDataAccess;
            _config = config;
        }
        public async Task<IEnumerable<MenuModel>> GetAllMenuAsync()
        {
            Utils.NLogMessage(GetType(), $"{"GetAllMenuAsync()"}", Utils.NLogType.Info);
            DataOracleParameters parameters = new DataOracleParameters();
            parameters.Add("RC1", OracleMappingType.RefCursor, ParameterDirection.Output);
            var menu = await _orcaleDataAccess.LoadDataAsync<MenuModel, dynamic>("GEN_SEL_MENU_SP", parameters.dynamicParameters, "OracleConnectionString");
            return menu;
        }
        public async Task CreateMenuAsync(MenuModel menu)
        {
            Utils.NLogMessage(GetType(), $"{"CreateMenuAsync()"}", Utils.NLogType.Info);
            try
            {
                DataOracleParameters parameters = new DataOracleParameters();
                parameters.Add("P_PARENTMENUID", OracleMappingType.Int32, ParameterDirection.Input, menu.ParentMenuId);
                parameters.Add("P_PAGELINK", OracleMappingType.Varchar2, ParameterDirection.Input, menu.PageLink);
                parameters.Add("P_MENUNAME", OracleMappingType.Varchar2, ParameterDirection.Input, menu.MenuName);
                parameters.Add("P_ICONNAME", OracleMappingType.Varchar2, ParameterDirection.Input, menu.IconName);
                parameters.Add("P_PAGEROLES", OracleMappingType.Varchar2, ParameterDirection.Input, menu.PageRoles);
                parameters.Add("P_RECORDSTATUS", OracleMappingType.Varchar2, ParameterDirection.Input, menu.RecordStatus);
                await _orcaleDataAccess.SaveDataAsync("GEN_INS_MENU_SP", parameters.dynamicParameters, "OracleConnectionString");
            }
            catch (Exception e)
            {
                Utils.NLogMessage(GetType(), $" {e.Message} - {e.InnerException}", Utils.NLogType.Error);
            }
        }

        public async Task<MenuModel> GetMenuByPageLink(string pageLink)
        {
            Utils.NLogMessage(GetType(), $"{"GetMenuByPageLink()"}", Utils.NLogType.Info);
            DataOracleParameters parameters = new DataOracleParameters();
            parameters.Add("P_PAGELINK", OracleMappingType.Varchar2, ParameterDirection.Input, pageLink);
            parameters.Add("RC1", OracleMappingType.RefCursor, ParameterDirection.Output);
            var menu = await _orcaleDataAccess.LoadDataAsync<MenuModel, dynamic>("GEN_SEL_MENU_BYPAGELINK_SP", parameters.dynamicParameters, "OracleConnectionString");
            return menu.FirstOrDefault();
        }
    }
}
