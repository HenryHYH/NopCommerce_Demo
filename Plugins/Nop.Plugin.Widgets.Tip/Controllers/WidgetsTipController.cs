using System.Web.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Plugin.Widgets.Tip.Models;
using Nop.Services.Configuration;
using Nop.Services.Media;
using Nop.Services.Stores;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.Tip.Controllers
{
    public class WidgetsTipController : BasePluginController
    {
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly IStoreService _storeService;
        private readonly ISettingService _settingService;

        public WidgetsTipController(IWorkContext workContext,
            IStoreContext storeContext,
            IStoreService storeService,
            ISettingService settingService)
        {
            this._workContext = workContext;
            this._storeContext = storeContext;
            this._storeService = storeService;
            this._settingService = settingService;
        }

        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure()
        {
            //load settings for a chosen store scope
            var storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
            var tipSettings = _settingService.LoadSetting<TipSettings>(storeScope);
            var model = new ConfigurationModel();

            if (storeScope > 0)
            {

            }

            return View("Nop.Plugin.Widgets.Tip.Views.WidgetsTip.Configure", model);
        }

        [HttpPost]
        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure(ConfigurationModel model)
        {
            //load settings for a chosen store scope
            var storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
            var tipSettings = _settingService.LoadSetting<TipSettings>(storeScope);            

            /* We do not clear cache after each setting update.
             * This behavior can increase performance because cached settings will not be cleared 
             * and loaded from database after each update */
            
            //now clear settings cache
            _settingService.ClearCache();
            
            return Configure();
        }

        [ChildActionOnly]
        public ActionResult PublicInfo(string widgetZone)
        {
            var tipSettings = _settingService.LoadSetting<TipSettings>(_storeContext.CurrentStore.Id);

            var model = new PublicInfoModel();

            return View("Nop.Plugin.Widgets.Tip.Views.WidgetsTip.PublicInfo", model);
        }
    }
}