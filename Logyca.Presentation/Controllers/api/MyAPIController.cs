using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logyca.BLL;
using Logyca.DAL;
using Logyca.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Logyca.Presentation.Controllers.api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MyAPIController : Controller
    {
        private readonly DbContextOptions<LogycaDbContext> options;
        private EnterpriseBLL oEnterprise;
        private CodeBLL oCode;
        public MyAPIController(DbContextOptions<LogycaDbContext> options)
        {
            this.options = options;
        }

        #region Enterprise
        [HttpPost]
        public IActionResult InsertEnterprise(Enterprise model)
        {
            Response<bool> response = new Response<bool>();
            try
            {
                oEnterprise = new EnterpriseBLL(this.options);
                var resp = oEnterprise.Insert(model);
                if (resp == "success")
                {
                    response.Data = true;
                    response.IsSuccess = true;
                    response.Message = "Registro insertado exitosamente";
                    return Ok(response);
                }
                else
                {
                    response.Data = false;
                    response.IsSuccess = false;
                    response.Message = resp;
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.IsSuccess = false;
                response.Message = ex.Message;
                return Ok(response);
            }
        }

        [HttpPost]
        public IActionResult UpdateEnterprise(Enterprise model)
        {
            Response<bool> response = new Response<bool>();
            try
            {
                oEnterprise = new EnterpriseBLL(this.options);
                var resp = oEnterprise.Update(model);
                if (resp == "success")
                {
                    response.Data = true;
                    response.IsSuccess = true;
                    response.Message = "Registro actualizado exitosamente";
                    return Ok(response);
                }
                else
                {
                    response.Data = false;
                    response.IsSuccess = false;
                    response.Message = resp;
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.IsSuccess = false;
                response.Message = ex.Message;
                return Ok(response);
            }
        }

        [HttpPatch("{Id}")]
        public IActionResult UpdateFieldEnterprise(int Id, [FromBody] JsonPatchDocument<Enterprise> model)
        {
            var context = new LogycaDbContext(this.options);

            if (model == null)
            {
                return BadRequest();
            }

            var enterprise = context.Enterprises.FirstOrDefault(x => x.Id.Equals(Id)); //oEnterprise.GetEnterpriseById(Id);
            if (enterprise == null)
            {
                return NotFound();
            }

            model.ApplyTo(enterprise, ModelState);
            var isValid = TryValidateModel(enterprise);
            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            context.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Recuperar todas las empresas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllEnterprise()
        {
            Response<List<Enterprise>> response = new Response<List<Enterprise>>();
            try
            {
                oEnterprise = new EnterpriseBLL(this.options);
                var resp = oEnterprise.GetAllEnterprise();

                response.Data = resp;
                response.IsSuccess = true;
                response.Message = string.Empty;

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.IsSuccess = false;
                response.Message = ex.Message;

                return BadRequest(response);
            }

        }

        /// <summary>
        /// Recuperar una empresa con un nit especifico y sus códigos asociados.
        /// </summary>
        /// <param name="Nit">Nit de la empresa a buscar</param>
        /// <returns></returns>
        [HttpGet("{Nit}")]
        public IActionResult GetEnterpriseByNit(int Nit)
        {
            Response<List<Enterprise>> response = new Response<List<Enterprise>>();
            try
            {
                oEnterprise = new EnterpriseBLL(this.options);
                var resp = oEnterprise.GetEnterpriseByNit(Nit);
                response.Data = resp;
                response.IsSuccess = true;
                response.Message = string.Empty;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.IsSuccess = false;
                response.Message = ex.Message;

                return BadRequest(response);
            }

        }

        /// <summary>
        /// Recuperar la información de la empresa dueña de un código, usando el id del código.
        /// </summary>
        /// <param name="IdCode">Id del registro en la tabla Code</param>
        /// <returns></returns>
        [HttpGet("{IdCode}")]
        public IActionResult GetEnterpriseByIdCode(int IdCode)
        {
            Response<List<Enterprise>> response = new Response<List<Enterprise>>();
            try
            {
                oEnterprise = new EnterpriseBLL(this.options);
                var resp = oEnterprise.GetEnterpriseByIdCode(IdCode);

                response.Data = resp;
                response.IsSuccess = true;
                response.Message = string.Empty;

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.IsSuccess = false;
                response.Message = ex.Message;

                return BadRequest(response);
            }
        }
        #endregion

        #region Code

        [HttpPost]
        public IActionResult InsertCode(Code model)
        {
            Response<bool> response = new Response<bool>();
            try
            {
                oCode = new CodeBLL(this.options);
                var resp = oCode.Insert(model);
                if (resp == "success")
                {
                    response.Data = true;
                    response.IsSuccess = true;
                    response.Message = "Registro insertado exitosamente";
                    return Ok(response);
                }
                else
                {
                    response.Data = false;
                    response.IsSuccess = false;
                    response.Message = resp;
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.IsSuccess = false;
                response.Message = ex.Message;
                return Ok(response);
            }
        }

        [HttpPost]
        public IActionResult UpdateCode(Code model)
        {
            Response<bool> response = new Response<bool>();
            try
            {
                oCode = new CodeBLL(this.options);
                var resp = oCode.Update(model);
                if (resp == "success")
                {
                    response.Data = true;
                    response.IsSuccess = true;
                    response.Message = "Registro actualizado exitosamente";
                    return Ok(response);
                }
                else
                {
                    response.Data = false;
                    response.IsSuccess = false;
                    response.Message = resp;
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.IsSuccess = false;
                response.Message = ex.Message;
                return Ok(response);
            }
        }

        [HttpPatch("{Id}")]
        public IActionResult UpdateFieldCode(int Id, [FromBody] JsonPatchDocument<Code> model)
        {
            var context = new LogycaDbContext(this.options);

            if (model == null)
            {
                return BadRequest();
            }

            var code = context.Codes.FirstOrDefault(x => x.Id.Equals(Id)); //oEnterprise.GetEnterpriseById(Id);
            if (code == null)
            {
                return NotFound();
            }

            model.ApplyTo(code, ModelState);
            var isValid = TryValidateModel(code);
            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            context.SaveChanges();

            return NoContent();
        }


        /// <summary>
        /// Recuperar todos lo códigos pertenecientes a una empresa usando su id.
        /// </summary>
        /// <param name="Id">el Owner de la tabla Code</param>
        /// <returns>Retorna una lista de Code</returns>
        [HttpGet("{Id}")]
        public IActionResult GetCodeByEnterprise(int Id)
        {
            Response<List<Code>> response = new Response<List<Code>>();
            try
            {
                oCode = new CodeBLL(this.options);
                var resp = oCode.GetCodeByEnterprise(Id);

                response.Data = resp;
                response.IsSuccess = true;
                response.Message = string.Empty;

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.IsSuccess = false;
                response.Message = ex.Message;

                return BadRequest(response);
            }
        }
        #endregion

    }
}