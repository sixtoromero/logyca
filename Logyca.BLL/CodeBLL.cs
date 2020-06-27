using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Logyca.Models;
using Logyca.DAL;

namespace Logyca.BLL
{
    public class CodeBLL
    {
        private readonly DbContextOptions<LogycaDbContext> options;
        public CodeBLL(DbContextOptions<LogycaDbContext> options = null)
        {
            this.options = options;
        }

        public string Insert(Code model)
        {
            try
            {
                string message = string.Empty;
                using (var context = new LogycaDbContext(this.options))
                {
                    Code result = new Code();
                    result = context.Codes.Where(e => e.Id.Equals(model.Id) || e.Name.Equals(model.Name)).FirstOrDefault();
                    if (result == null)
                    {
                        context.Codes.Add(model);
                        context.SaveChanges();
                        message = "success";
                    }
                    else
                    {
                        message = "El registro ya existe";
                    }

                    return message;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Update(Code model)
        {
            try
            {
                string message = string.Empty;
                using (var context = new LogycaDbContext(this.options))
                {
                    Code result = new Code();
                    result = context.Codes.Where(e => e.Id.Equals(model.Id)).FirstOrDefault();
                    if (result != null)
                    {
                        result.Name = model.Name;
                        context.Entry(result).State = EntityState.Modified;

                        context.SaveChanges();
                        message = "success";
                    }
                    else
                    {
                        message = "El Nit ya existe";
                    }

                    return message;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<Code> GetCodeByEnterprise(int Id)
        {
            try
            {
                List<Code> result = new List<Code>();
                using (var context = new LogycaDbContext(this.options))
                {
                    result = context.Codes.Where(c => c.Owner.Equals(Id)).ToList();
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
