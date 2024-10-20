var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Employee> employees = new List<Employee>()
{
    new("Bobby", 25),
    new("Sammy", 31),
    new("Jimmy", 19),
};


//app.MapPost("/data", async (HttpContext context) => 
//{

//    //using StreamReader reader = new StreamReader(context.Request.Body);
//    //string message = await reader.ReadToEndAsync();

//    //Console.WriteLine($"Message: {message}");
//    //return $"Message: {message}";
//});

app.MapPost("/new", (Employee employeeData) =>
{
    Employee employee = new Employee(employeeData.Name, employeeData.Age);
    employees.Add(employee);

    return employee;
});

app.MapGet("/", () => employees);

app.Run();

class Employee
{
    static int globalId = 0;

    public int Id { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }

    public Employee(string name, int age)
    {
        Id = ++globalId;
        Name = name;
        Age = age;
    }
}