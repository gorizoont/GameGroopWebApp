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

        public EventsController(IEventsRepository eventsRepository, IPhotoService photoService)
        {
            _eventsRepository = eventsRepository;
            _photoService = photoService;
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
            return View();
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
    }
}
