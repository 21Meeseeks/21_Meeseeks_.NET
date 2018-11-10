# 21_Meeseeks_.NET
## Welcome to the 2nd phase  <br>
This is the First step to start this new challenge . <br>
To be clear from the start , i made a simple project containing the entities generated from the existant database , then i put the suitable architecture for the project . <br>
after that i tried to consume a service , " display all projects " by adding this lines .<br>
 ```csharp
      HttpClient Client = new HttpClient();
      Client.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/"); => to link to the Address of the service  
      Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); => to add the header
      
      HttpResponseMessage response = await Client.GetAsync("rest/project"); => to get the response
      string data = await response.Content.ReadAsStringAsync();
      JavaScriptSerializer JSserializer = new JavaScriptSerializer();
      ViewBag.result = JSserializer.Deserialize<IEnumerable<Project>>(data); => deserialize the json response into a collection of projects
```
But before that i had to install on the WebMAP Project the System.Net.Http.Formatting package . <br>
the project is imported from the Model Folder , that contains some informations about the project . <br>
then i started the service and i tested by getting to the URL : localhost:port/test<br>
and fortunatly i got the result i'm looking for . <br>
i didn't test the other services , with post or delete methods . But if someone'll have a problem , if he finds a solution tell us , same if he deosn't . <br>
Check the EntityFramework and the mysql.date package . And also check the condition of his services first .
