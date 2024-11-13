using HotelBookingSystem.Application.Converters;
using HotelBookingSystem.Application.DTOs;
using HotelBookingSystem.Application.Helper.Exceptions;
using HotelBookingSystem.Application.Interfaces;
using HotelBookingSystem.Application.Services;
using HotelBookingSystem.Persistence.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace HotelBookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBooking _bookingService;

        public BookingController(IBooking bookingRepository) 
        {
            _bookingService = bookingRepository;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddBooking([FromBody] BookingDTO bookingDto)
        {
            // Basic validation
            try
            {
                if (bookingDto.CheckInDate >= bookingDto.CheckOutDate)
                {
                    bool isRoomAvailable = await _bookingService.IsRoomAvailable(bookingDto);
                    if (!isRoomAvailable)
                    {
                        //return Conflict("The room is already booked for the selected dates.");
                        throw new BaseException("The room is already booked for the selected dates.");

                    }
                    _bookingService.Add(BookingConverter.Booking(bookingDto));
                    _bookingService.Save();
                    //return BadRequest("Check-out date must be after check-in date.");
                }
                return Ok(new {  succsess = "Created successfully" });
            }
            catch (Exception ex)
            {

                throw new BaseException("The room is already booked for the selected dates.");
            }
        }

    }
}
