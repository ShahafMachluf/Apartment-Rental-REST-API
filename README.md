# Apartment Rental REST API

The following REST API allows you to:

1. Create guest user.
2. Create host user with his apartment details.
3. Book an apartment.
4. Search for the most popular apartments in a selected country and city.

## Installation

-  Update SQL Server connection string in ```appsettings.json```
- Create the database using the following command: ```dotnet ef database update --context ReservationContext```




## Available methods

| Method| Resource | Action    |
| ------------- | ------------- |-----------|
| GET   | /api/Guests   |Get all guests |
| GET   | /api/Guests/{int}  |Get guest detailes with specific id |
| POST  | /api/Guests?name={string}&birthday={date} |Create new guest |
| GET   | /api/Hosts  |Get all hosts|
| GET   | /api/Hosts/{int}  |Get host detailes with specific id |
| POST  | /api/Hosts  |Create new host with his apartment information|
| GET   | /api/Reservations  |Get all reservations|
| GET   | /api/Reservations/{int}  |Get reservation detailes with specific id |
| POST  | /api/Reservations?ArrivingDate={date}&LeavingDate={date}&ReservationHostId={int}&ReservationGuestId={int}  |Create new reservation |
| GET   | /api/Search?Country={string}&City={string} |Get the most popular apartments in the selected country and city|


### Create new host request body:

```json
{
  "apartment": {
    "address": {
      "country": "string",
      "city": "string",
      "street": "string"
    },
  }
}
```
