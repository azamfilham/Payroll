﻿using Payroll.DataModel;
using Payroll.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Repository
{
    public class JobPositionRepo
    {
        public static List<JobPositionViewModel> Get()
        {
            List<JobPositionViewModel> result = new List<JobPositionViewModel>();
            using (var db = new PayrollContext())
            {
                result = (from d in db.JobPosition
                          join dep in db.Department on
                          d.DepartmentId equals dep.Id
                          select new JobPositionViewModel
                          {
                              Id = d.Id,
                              Code = d.Code,
                              DepartmentId = d.DepartmentId,
                              DepartmentCode = dep.Code,
                              DepartmentName = dep.Description,
                              Description = d.Description,                              
                              IsActivated = d.IsActivated
                          }).ToList();
            }
            return result;
        }
        public static JobPositionViewModel GetById(int id)
        {
            JobPositionViewModel result = new JobPositionViewModel();
            using (var db = new PayrollContext())
            {
                result = (from d in db.JobPosition
                          join dep in db.Department on
                          d.DepartmentId equals dep.Id
                          where d.Id == id
                          select new JobPositionViewModel
                          {
                              Id = d.Id,
                              Code = d.Code,
                              DepartmentId = d.DepartmentId,
                              DepartmentCode = dep.Code,
                              DepartmentName = dep.Description,
                              Description = d.Description,                              
                              IsActivated = d.IsActivated
                          }).FirstOrDefault();
            }
            return result;
        }

        public static Responses Update(JobPositionViewModel entity)
        {
            Responses result = new Responses();
            try
            {
                using (var db = new PayrollContext())
                {
                    if (entity.Id != 0)
                    {
                        JobPosition jobposition = db.JobPosition.Where(o => o.Id == entity.Id).FirstOrDefault();
                        if (jobposition != null)
                        {
                            jobposition.Code = entity.Code;
                            jobposition.DepartmentId = entity.DepartmentId;
                            jobposition.Description = entity.Description;
                            jobposition.IsActivated = entity.IsActivated;
                            jobposition.ModifyBy = "Azam";
                            jobposition.ModifyDate = DateTime.Now;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        JobPosition jobposition = new JobPosition();
                        jobposition.Code = entity.Code;
                        jobposition.DepartmentId = entity.DepartmentId;
                        jobposition.Description = entity.Description;
                        jobposition.IsActivated = entity.IsActivated;
                        jobposition.CreateBy = "Azam";
                        jobposition.CreateDate = DateTime.Now;
                        db.JobPosition.Add(jobposition);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Success = false;
            }
            return result;
        }

        public static Responses Delete(int id)
        {
            Responses result = new Responses();
            try
            {
                using (var db = new PayrollContext())
                {
                    JobPosition jobposition = db.JobPosition.Where(o => o.Id == id).FirstOrDefault();
                    if (jobposition != null)
                    {
                        db.JobPosition.Remove(jobposition);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Success = false;
            }
            return result;
        }
    }
}
