# Holiday Booking API

## Overview

This Holiday Booking API is designed to facilitate the management of flight bookings for a holiday service. It is built using C# and .NET, following RESTful API principles.

## Features

- **Clean Architecture:** Utilizes a well-organized structure with models, controllers, and services for easy maintenance.
- **RESTful Design:** Follows RESTful API design principles to ensure scalability and interoperability.
- **Database Integration:** Connects to a SQL Server database using Entity Framework for efficient data storage.

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Setup

1. Clone the repository:

    ```bash
    git clone https://github.com/yussufshariff/holiday-booking-api.git
    ```

2. Navigate to the project directory:

    ```bash
    cd holiday-booking-api
    ```

3. Set up the database:

    ```bash
    dotnet ef database update
    ```

4. Run the application:

    ```bash
    dotnet run
    ```

The API will be accessible at `https://localhost:5001`.

## API Endpoints

- `GET /api/Bookings`: Retrieve all bookings.
- `POST /api/Bookings`: Create a new booking.
- `DELETE /api/Bookings/{id}`: Delete a booking by ID.
- `PATCH /api/Bookings/{id}`: Update a booking by ID
- `GET /api/Flights`: Retrieve all Flights and filter.
- `GET /api/Flights/{id}`: Retrieve a specfic Flight.
- `POST /api/User`: Create a new Account.
- And many more...


## Documentation

Explore the API using the Swagger documentation available at `https://localhost:5001/swagger`.

