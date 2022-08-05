# Question 01
  1) As I don't know what's the data source of the events, I'll mock a enumerable in a static method.
  2) I'll call a static method on the same file (program.cs)
  3) I'll throw a argument null exception
  4) Yes there is, first of all, it's not object orientated, for the sake of simplicity, and we could swap the exception for a domain notification

# Question 02
  1) I'm using the provided function, but in a real application, I would chose a geolocalization solution to find the distance based on the two location coordinates.
  2) I'll use a linq expression to sort by distance and take the top 5 rows in the sorted list
  3) I'll throw a argument null exception
  4) Yes there is, it's not object orientated, for the sake of simplicity, and we could use a geolocalization system to find the real distance between the cities

# Question 03
  I'm using a simple retry logic, could be made using polly nuget package, but I think that it would be dificult to use it on the test. 

# Question 04
  I'm defaulting the distance to zero if the function fails to return a valid value.

# Question 05
  Im using a where clause on the linq expression to filter by base price, same principle could be used for other filter formats.
