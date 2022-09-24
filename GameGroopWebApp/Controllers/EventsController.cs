using GameGroopWebApp.Interfaces;
using GameGroopWebApp.Models;
using GameGroopWebApp.Repository;
using GameGroopWebApp.Services;
using GameGroopWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
    }
}
