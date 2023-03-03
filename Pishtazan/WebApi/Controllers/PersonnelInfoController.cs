using Domain.Dtos;
using Domain.Entities;
using Domain.Services;
using Domain.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    /// <summary>
    /// مدیریت کارکنان
    /// </summary>
    [ApiController]
    [Route("api/v1/{dataType}/[controller]/[action]")]
    public class PersonnelInfoController : ControllerBase
    {
        private readonly IPersonnelInfoServices _personnalServices;
        public PersonnelInfoController(IPersonnelInfoServices personnalServices)
        {
            _personnalServices = personnalServices;
        }
        /// <summary>
        /// افزودن پروفایل جدید
        /// </summary>
        /// <param name="dto">مقدار</param>
        /// <returns></returns>
        /// /// <remarks>
        /// Sample request:
        ///
        ///     dataType : "csv"
        ///     POST /Add
        ///     {
    ///"data": "FirstName,LastName,BasicSalary,Allowance,Transportation,TotalSallary,SalaryDate \r\n Tanmay,Patil,1234567890,111,111,232424,2023-02-03 \r\n  Tanmay,Patil,1234567890,111,111,232424,2023-02-03",
   ///"calculatorName": "CalcurlatorA"
///}
    ///
    /// </remarks>

    [HttpPost]
        public async Task<IActionResult> Add(ProcessDataAndOverTimeCalculator dto)
        {
            List<PersonnelInfo> personnelInfos = _personnalServices.GetPersonnelInfoFromData(
                RouteData.Values["datatype"].ToString(),
                dto);
            foreach (var item in personnelInfos)
                await _personnalServices.InsertAsync(item, Request.HttpContext.RequestAborted);
            return Ok();
        }

        /// <summary>
        /// بروزرسانی پروفایل
        /// </summary>
        /// <param name="dto">مقدار</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Update(ProcessDataAndOverTimeCalculator dto)
        {
            List<PersonnelInfo> personnelInfos = _personnalServices.GetPersonnelInfoFromData(
                RouteData.Values["datatype"].ToString(),
                dto);
            await _personnalServices.UpdateRange(personnelInfos, Request.HttpContext.RequestAborted);
            return Ok();
        }

        /// <summary>
        /// حذف پروفایل
        /// </summary>
        /// <param name="dto">مقدار</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteReqDto dto)
        {
            return Ok(await _personnalServices.DeleteAsync(dto.Id, Request.HttpContext.RequestAborted));
        }

        /// <summary>
        /// دریافت لیست پروفایل
        /// </summary>
        /// <param name="dto">مقدار</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetRange(GetRangeDto dto)
        {
            DateTime from = dto.From.ToSystemDate();
            DateTime to = dto.To.ToSystemDate();

            return Ok(await _personnalServices.GetAllWithFilterAsync
                (dto.PageSize, dto.PageNumber, dto.AscSort, dto.SrtField,
                (x => x.SalaryDate >= from && x.SalaryDate <= to),
                Request.HttpContext.RequestAborted));
        }

        /// <summary>
        /// دریافت مقدار پروفایل
        /// </summary>
        /// <param name="dto">مقدار</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _personnalServices.GetOneAsync(id,Request.HttpContext.RequestAborted));
        }

    }
}
