using AutoMapper;
using HIMS.BL.Interfaces;
using HIMS.BL.Models;
using HIMS.Server.Models;
using HIMS.Server.utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HIMS.Server.Controllers
{
    public class SampleController : Controller
    {
        private readonly ISampleService _sampleService;

        public SampleController(ISampleService sampleService)
        {
            _sampleService = sampleService;
        }

        //[Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            IEnumerable<SampleDTO> sampleDtos = _sampleService.GetSamples();
            var samples = new SamplesListViewModel
            {
                Samples = Mapper.Map<IEnumerable<SampleDTO>, List<SampleViewModel>>(sampleDtos),
                SamplesAmount = _sampleService.GetSampleEntriesAmout(CurrentUser.IsAdmin)
            };

            return View(samples);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name, Description")]SampleViewModel sample)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var sampleDto = Mapper.Map<SampleViewModel, SampleDTO>(sample);
                    _sampleService.SaveSample(sampleDto);
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(sample);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SampleDTO sampleDto = _sampleService.GetSample(id);

            if (sampleDto == null)
            {
                return HttpNotFound();
            }

            var sample = Mapper.Map<SampleDTO, SampleViewModel>(sampleDto);
            return View(sample);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var sampleDto = _sampleService.GetSample(id);

            if (TryUpdateModel(sampleDto, "",
                new string[] { nameof(sampleDto.Name), nameof(sampleDto.Description) }))
            {
                try
                {
                    _sampleService.UpdateSample(sampleDto);

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            var sample = Mapper.Map<SampleDTO, SampleViewModel>(sampleDto);
            return View(sample);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SampleDTO sampleDto = _sampleService.GetSample(id);

            if (sampleDto == null)
            {
                return HttpNotFound();
            }

            var sample = Mapper.Map<SampleDTO, SampleViewModel>(sampleDto);
            return View(sample);
        }

        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }

            SampleDTO sampleDto = _sampleService.GetSample(id);

            if (sampleDto == null)
            {
                return HttpNotFound();
            }

            var sample = Mapper.Map<SampleDTO, SampleViewModel>(sampleDto);

            return View(sample);
        }

        // POST: Student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                _sampleService.DeleteSample(id);
            }
            catch (RetryLimitExceededException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }
    }
}