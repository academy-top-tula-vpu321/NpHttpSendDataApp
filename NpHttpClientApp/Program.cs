using System.Net.Http.Json;

HttpClient client = new();

//StringContent content = new("Hello world! Hello people!");

//var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7292/data");
//request.Content = content;

//using var response = await client.SendAsync(request);

//string serverResponse = await response.Content.ReadAsStringAsync();
//Console.WriteLine(serverResponse);

string host = "https://localhost:7292";

while(true)
{
    Console.WriteLine("1: View All");
    Console.WriteLine("2: Add New");
    Console.WriteLine("0: Exit");
    Console.Write("Your choise: ");
    var answer = Console.ReadLine();

    if (answer == "0") break;

    if(answer == "1")
    {
        using var response = await client.GetAsync(host + "/");
        var employees = await response.Content.ReadFromJsonAsync<List<Employee>>();

        foreach(var e in employees)
            Console.WriteLine($"{e.Id} {e.Name} {e.Age}");
    }
    else if(answer == "2")
    {
        Console.Write("Input name: ");
        string? name = Console.ReadLine();
        Console.Write("Input age: ");
        int age = Int32.Parse(Console.ReadLine()!);

        //JsonContent content = JsonContent.Create(new Employee() { Name = name, Age = age});
        //using var response = await client.PostAsync(host + "/new", content);
        //Employee? employee = await response.Content.ReadFromJsonAsync<Employee>();

        using var response = await client.PostAsJsonAsync(host + "/new",
                                                    new Employee() { Name = name, Age = age });

        Employee? employee = await response.Content.ReadFromJsonAsync<Employee>();
        Console.WriteLine($"New employee: {employee.Id} {employee.Name} {employee.Age}");
    }
}


class Employee
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }
}