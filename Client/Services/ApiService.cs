using System.Net.Http.Json;
using CommunityEvents.Models;

namespace Client.Services
{
    public class ApiService
    {
        private readonly HttpClient _http;

        public ApiService(HttpClient http) => _http = http;

        // ── Events ──────────────────────────────────────────
        public Task<List<Event>?> GetEventsAsync() =>
            _http.GetFromJsonAsync<List<Event>>("api/events");

        public Task<Event?> GetEventAsync(int id) =>
            _http.GetFromJsonAsync<Event>($"api/events/{id}");

        public Task<Event?> CreateEventAsync(Event ev) =>
            _http.PostAsJsonAsync("api/events", ev)
                 .ContinueWith(t => t.Result.Content.ReadFromJsonAsync<Event>().Result);

        public Task<Event?> UpdateEventAsync(Event ev) =>
            _http.PutAsJsonAsync($"api/events/{ev.Id}", ev)
                 .ContinueWith(t => t.Result.Content.ReadFromJsonAsync<Event>().Result);

        public Task DeleteEventAsync(int id) =>
            _http.DeleteAsync($"api/events/{id}");

        // ── Venues ──────────────────────────────────────────
        public Task<List<Venue>?> GetVenuesAsync() =>
            _http.GetFromJsonAsync<List<Venue>>("api/venues");

        public Task<Venue?> CreateVenueAsync(Venue venue) =>
            _http.PostAsJsonAsync("api/venues", venue)
                 .ContinueWith(t => t.Result.Content.ReadFromJsonAsync<Venue>().Result);

        public Task<Venue?> UpdateVenueAsync(Venue venue) =>
            _http.PutAsJsonAsync($"api/venues/{venue.Id}", venue)
                 .ContinueWith(t => t.Result.Content.ReadFromJsonAsync<Venue>().Result);

        public Task DeleteVenueAsync(int id) =>
            _http.DeleteAsync($"api/venues/{id}");

        // ── Activities ──────────────────────────────────────
        public Task<List<Activity>?> GetActivitiesAsync() =>
            _http.GetFromJsonAsync<List<Activity>>("api/activities");

        public Task<Activity?> CreateActivityAsync(Activity activity) =>
            _http.PostAsJsonAsync("api/activities", activity)
                 .ContinueWith(t => t.Result.Content.ReadFromJsonAsync<Activity>().Result);

        public Task<Activity?> UpdateActivityAsync(Activity activity) =>
            _http.PutAsJsonAsync($"api/activities/{activity.Id}", activity)
                 .ContinueWith(t => t.Result.Content.ReadFromJsonAsync<Activity>().Result);

        public Task DeleteActivityAsync(int id) =>
            _http.DeleteAsync($"api/activities/{id}");

        // ── Participants ─────────────────────────────────────
        public Task<List<Participant>?> GetParticipantsAsync() =>
            _http.GetFromJsonAsync<List<Participant>>("api/participants");

        public Task<Participant?> CreateParticipantAsync(Participant participant) =>
            _http.PostAsJsonAsync("api/participants", participant)
                 .ContinueWith(t => t.Result.Content.ReadFromJsonAsync<Participant>().Result);

        public Task<Participant?> UpdateParticipantAsync(Participant participant) =>
            _http.PutAsJsonAsync($"api/participants/{participant.Id}", participant)
                 .ContinueWith(t => t.Result.Content.ReadFromJsonAsync<Participant>().Result);

        public Task DeleteParticipantAsync(int id) =>
            _http.DeleteAsync($"api/participants/{id}");

        // ── Registrations ────────────────────────────────────
        public Task<List<Registration>?> GetRegistrationsAsync() =>
            _http.GetFromJsonAsync<List<Registration>>("api/registrations");

        public Task<List<Registration>?> GetRegistrationsByParticipantAsync(int participantId) =>
            _http.GetFromJsonAsync<List<Registration>>($"api/registrations/participant/{participantId}");

        public Task<Registration?> CreateRegistrationAsync(Registration registration) =>
            _http.PostAsJsonAsync("api/registrations", registration)
                 .ContinueWith(t => t.Result.Content.ReadFromJsonAsync<Registration>().Result);

        public Task DeleteRegistrationAsync(int id) =>
            _http.DeleteAsync($"api/registrations/{id}");
    }
}