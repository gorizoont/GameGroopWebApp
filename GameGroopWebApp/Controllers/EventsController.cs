using CloudinaryDotNet.Actions;
using GameGroopWebApp.Interfaces;
using GameGroopWebApp.Models;
using GameGroopWebApp.Repository;
using GameGroopWebApp.Services;
using GameGroopWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GameGroopWebApp.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventsRepository _eventsRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EventsController(IEventsRepository eventsRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _eventsRepository = eventsRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Events> events = await _eventsRepository.GetAll();
            return View(events);
        }
        public async Task<IActionResult> Detail(int id)
        {
            Events events = await _eventsRepository.GetByIdAsync(id);
            return View(events);
        }
        public IActionResult Create()
        {
            var curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var createEventsViewModel = new CreateEventsViewModel { AppUserId = curUserId };
            return View(createEventsViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateEventsViewModel eventsVM)
        {
            if (ModelState.IsValid)
            {
                var uploadResult = await _photoService.AddPhotoAsync(eventsVM.Image);

                var events = new Events
                {
                    Title = eventsVM.Title,
                    Description = eventsVM.Description,
                    Image = uploadResult.Url.ToString(),
                    EventsCategory = eventsVM.EventsCategory,
                    AppUserId = eventsVM.AppUserId,
                    Address = new Address
                    {
                        Street = eventsVM.Address.Street,
                        City = eventsVM.Address.City,
                        State = eventsVM.Address.State,
                    }
                };
                _eventsRepository.Add(events);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }

            return View(eventsVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var events = await _eventsRepository.GetByIdAsync(id);
            if (events == null) return View("Error");
            var eventsVM = new EditEventsViewModel
            {
                Title = events.Title,
                Description = events.Description,
                AddressId = events.AddressId,
                Address = events.Address,
                URL = events.Image,
                EventsCategory = events.EventsCategory
            };
            return View(eventsVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditEventsViewModel eventsVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit Events");
                return View("Edit", eventsVM);
            }

            var userClub = await _eventsRepository.GetByIdAsyncNoTracking(id);

            if (userClub == null)
            {
                return View("Error");
            }

            var photoResult = await _photoService.AddPhotoAsync(eventsVM.Image);

            if (photoResult.Error != null)
            {
                ModelState.AddModelError("Image", "Photo upload failed");
                return View(eventsVM);
            }

            var events = new Events
            {
                Id = id,
                Title = eventsVM.Title,
                Description = eventsVM.Description,
                Image = photoResult.Url.ToString(),
                AddressId = eventsVM.AddressId,
                Address = eventsVM.Address,
                EventsCategory = eventsVM.EventsCategory
            };

            _eventsRepository.Update(events);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var clubDetails = await _eventsRepository.GetByIdAsync(id);
            if (clubDetails == null) return View("Error");
            return View(clubDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var clubDetails = await _eventsRepository.GetByIdAsync(id);
            if (clubDetails == null)
            {
                return View("Error");
            }

            if (!string.IsNullOrEmpty(clubDetails.Image))
            {
                _ = _photoService.DeletePhotoAsync(clubDetails.Image);
            }

            _eventsRepository.Delete(clubDetails);
            return RedirectToAction("Index");
        }
    }
}
