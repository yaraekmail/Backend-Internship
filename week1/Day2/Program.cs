//task 1. Write a console program with at least 3 value-type and 3 reference-type variables, printing each one's type using"GetType()".

int x=5;
bool y=true;
double z=550.2;
string s="yara";
int [] num={1,2,3,4,};
string [] name={"yara","eyad","kmail"};

Console.WriteLine("=== Value Types ===");
Console.WriteLine($"x : {x.GetType()}");
Console.WriteLine($"y : {y.GetType()}");
Console.WriteLine($"z : {z.GetType()}");
//
Console.WriteLine();

Console.WriteLine("=== Reference Types ===");
Console.WriteLine($"s : {s.GetType()}");
Console.WriteLine($"num : {num.GetType()}");
Console.WriteLine($"name : {name.GetType()}");


Console.WriteLine();
Console.WriteLine();
//task2 2. Write a method that demonstrates the value-vs-reference copy behavior, printing before and after a mutation.
void valuetype()
{
 Console.WriteLine("valuetype")  ;
 int x=33;
 int y=x;
   Console.WriteLine("before change") ;
    Console.WriteLine($"x= {x}")  ;
        Console.WriteLine($"y={y}")  ;
        y=22;

           Console.WriteLine("after change") ;
    Console.WriteLine($"x= {x}")  ;
        Console.WriteLine($"y={y}")  ;

}


void refrencetype()
{
 Console.WriteLine("refrencetype")  ;
 int [] yara={1,2,3,4};
 int []kmail=yara;
   Console.WriteLine("before change") ;
    Console.WriteLine($"yara[0]= {yara[0]}")  ;
       Console.WriteLine($"kmail[0]= {kmail[0]}")  ;
    
kmail[0]=1289;
           Console.WriteLine("after change") ;
   Console.WriteLine($"yara[0]= {yara[0]}")  ;
    Console.WriteLine($"kmail[0]= {kmail[0]}")  ;
   

}


valuetype();
Console.WriteLine();
refrencetype();

Console.WriteLine();
Console.WriteLine();
//task3. Write a grade-classifier method using a switch expression, covering at least 4 score ranges.
void grade(int score)
{
    string grade =score switch
    {
        >= 90 =>"excelent",
          >= 80=>"very good",
            >= 50 =>"pass",
            <=49 =>"fail"
    };
    Console.WriteLine($"score:{score} >>{grade}");
}
grade(99);
grade(88);
grade(70);
grade(30);
Console.WriteLine();
Console.WriteLine();
//task4. Write a small program that reads user input and handles a possibly-null value safely, with nullable reference typesenabled.
Console.WriteLine("enter your name:");
string? names=Console.ReadLine();
if(names!=null)
Console.WriteLine(names);
else
Console.WriteLine("no name ");