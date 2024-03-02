using Microsoft.AspNetCore.Mvc;
using GustWeb.DataAcess.Data;
using Gustf.Models;
using Gustf.DataAcess.Repository.IRepository;
using Gustf.DataAcess.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Gustf.Models.ViewModels;
using Gustf.Utility;
using Microsoft.AspNetCore.Authorization;


namespace GustWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
           
            return View(objCompanyList);
        }

        // Quando não é declarado nada como no ex:[HttpPost] , por padrão é GET
        public IActionResult Upsert(int? id)
        {
          
            if(id == null || id == 0 )
            {
                //create
                return View(new Company());
            }
            else
            {
                //update
               Company companyObj = _unitOfWork.Company.Get(u=>u.Id==id);
                return View(companyObj);
            }
            
        }

        [HttpPost]
        public IActionResult Upsert(Company CompanyObj)
        {

            if (ModelState.IsValid)
            {
              
                
                if(CompanyObj.Id == 0)
                {
                    _unitOfWork.Company.Add(CompanyObj);
                    
                }
                else
                {
                    _unitOfWork.Company.Update(CompanyObj);
                }

                _unitOfWork.Save();
                TempData["success"] = "Company created sucessfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(CompanyObj);
            }
            
        }

        // Quando não é declarado nada como no ex:[HttpPost] , por padrão é GET
        public IActionResult Edit(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }

            Company? CompanyFromDb = _unitOfWork.Company.Get(u => u.Id == id);
            //Category categoryFromDb2 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Category categoryFromDb3 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();

            if (CompanyFromDb == null)
            {
                return NotFound();
            }

            return View(CompanyFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Company obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Company.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Company edited sucessfully";
                return RedirectToAction("Index");
            }

            return View();
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = objCompanyList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var CompanyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);
            if (CompanyToBeDeleted == null)
            {
                return Json(new { sucess = false, message = "Error while deleting" });
            }

            _unitOfWork.Company.Remove(CompanyToBeDeleted);
            _unitOfWork.Save();

            return Json(new { sucess = true, message = "Delete Sucessful" });

        }
        #endregion
    }


}