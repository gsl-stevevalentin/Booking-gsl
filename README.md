# Book a hotel room

## Spec
As an unauthenticated user, I can look the room list
	a room has an identifiant, name, price, booking schedule, maxPersons
	
As an unauthenticated user, I can create a account
	an account has an identifiant, username, email, password
	new account should receive a welcome message
	
As an authenticated user, I can book a room
	the schedule cannot be more than 48h
	the room must be available
	the booking must contain less persons than the room capability
	the account should receive a confirmation message
	
As an authenticated user, I can see my bookings


RoomId   -    StartDate     -    EndDate  
202 