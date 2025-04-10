using System;
using System.Collections.Generic;
using System.Reflection;
using CreateAutoMapper;

// Main program to demonstrate our simplified custom mapper
var mapper = new SimpleMapper();

// Register mappings
mapper.CreateMap<Person, PersonDto>(config => {
    config.MapProperty("Id", "Id");
    config.MapProperty("Name", "FullName");
    config.MapProperty("Email", "EmailAddress");
});

// Create a source object
var person = new Person
{
    Id = 1,
    Name = "John Doe",
    Email = "john@example.com",
    Age = 30
};

// Map to destination
var personDto = mapper.Map<Person, PersonDto>(person);

// Display results
Console.WriteLine("Source: Id={0}, Name={1}, Email={2}, Age={3}", 
    person.Id, person.Name, person.Email, person.Age);
Console.WriteLine("Destination: Id={0}, FullName={1}, EmailAddress={2}", 
    personDto.Id, personDto.FullName, personDto.EmailAddress);

// Simple mapper implementation
