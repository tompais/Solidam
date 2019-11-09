using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using Helpers;
using Services;

namespace Solidam.Controllers
{
    public class DonacionesApiController : ApiController
    {
        [HttpPost]
        public JsonResult<object> GetDonaciones(int id)
        {
            var draw = (HttpContext.Current.Request.Form.GetValues("draw") ?? throw new InvalidOperationException()).FirstOrDefault();
            var start = (HttpContext.Current.Request.Form.GetValues("start") ?? throw new InvalidOperationException()).FirstOrDefault();
            var length = (HttpContext.Current.Request.Form.GetValues("length") ?? throw new InvalidOperationException()).FirstOrDefault();
            var sortColumn = (HttpContext.Current.Request.Form.GetValues("columns[" + (HttpContext.Current.Request.Form.GetValues("order[0][column]") ?? throw new InvalidOperationException()).FirstOrDefault() + "][name]") ?? throw new InvalidOperationException()).FirstOrDefault();
            var sortColumnDir = (HttpContext.Current.Request.Form.GetValues("order[0][dir]") ?? throw new InvalidOperationException()).FirstOrDefault();
            var searchValue = (HttpContext.Current.Request.Form.GetValues("search[value]") ?? throw new InvalidOperationException()).FirstOrDefault()?.ToLower().Trim();
            var pager = new Pager(start != null ? Convert.ToUInt32(start) : default, length != null ? Convert.ToUInt32(length) : default);
            var sorter = new Sorter(sortColumn, sortColumnDir);
            var data = DonacionesService.Instance.GetAllDonacionesDTOByFilter(id, searchValue, sorter, pager);
            var recordsTotal = DonacionesService.Instance.GetAllCount(id);
            var recordsFiltered = data.Count;
            return Json<object>(new { draw, recordsFiltered, recordsTotal, data });
        }
    }
}