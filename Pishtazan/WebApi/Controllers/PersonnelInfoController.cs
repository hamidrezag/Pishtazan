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
        ///         "data": "FirstName,LastName,BasicSalary,Allowance,Transportation,TotalSallary,SalaryDate \r\n Tanmay,Patil,1234567890,111,111,232424,14000101 \r\n  Tanmay,Patil,1234567890,111,111,232424,14000202",
        ///         "calculatorName": "CalcurlatorA"
        ///     }
    ///
    /// </remarks>

    [HttpPost("api/v1/{dataType}/[controller]/[action]")]
        public async Task<IActionResult> Add([FromBody]ProcessDataAndOverTimeCalculator dto)
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
        /// /// <remarks>
        /// Sample request:
        ///
        ///     dataType : "csv"
        ///     POST /Add
        ///     {
        ///         "data": "Id,FirstName,LastName,BasicSalary,Allowance,Transportation,TotalSallary,SalaryDate \r\n 1,Tanmay,Patil,1234567890,111,111,232424,14000101 \r\n  2,Tanmay,Patil,1234567890,111,111,232424,14000202",
        ///         "calculatorName": "CalcurlatorA"
        ///     }
        ///
        /// </remarks>
        [HttpPost("api/v1/{dataType}/[controller]/[action]")]
        public async Task<IActionResult> Update([FromBody] ProcessDataAndOverTimeCalculator dto)
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
        [HttpDelete("api/v1/[controller]/[action]/{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteReqDto dto)
        {
            return Ok(await _personnalServices.DeleteAsync(dto.Id, Request.HttpContext.RequestAborted));
        }

        /// <summary>
        /// دریافت لیست پروفایل
        /// </summary>
        /// <param name="dto">فیلتر</param>
        /// <returns></returns>
        [HttpGet("api/v1/[controller]/[action]")]
        public async Task<IActionResult> GetRange([FromQuery]GetRangeDto dto)
        {
            DateTime from = dto.From.ToSystemDate();
            DateTime to = dto.To.ToSystemDate();

            return Ok(await _personnalServices.GetAllAsyncWithDapper(
                dto.PageSize,
                dto.PageNumber,
                dto.AscSort,
                dto.SrtField,
                from,to,
                HttpContext.RequestAborted
                ));

            //return Ok(await _personnalServices.GetAllWithFilterAsync
            //    (dto.PageSize, dto.PageNumber, dto.AscSort, dto.SrtField,
            //    (x => x.SalaryDate >= from && x.SalaryDate <= to),
            //    Request.HttpContext.RequestAborted));
        }

        /// <summary>
        /// دریافت مقدار پروفایل
        /// </summary>
        /// <param name="dto">مقدار</param>
        /// <returns></returns>
        [HttpGet("api/v1/[controller]/[action]/{id}")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            return Ok(await _personnalServices.GetOneAsync(id,Request.HttpContext.RequestAborted));
        }

    }
}
