# Premium Calculator
Premium Calculator is an application that help us to calculate specific amounts depends on defined rules based on birthdate, age and US state belonging. 
This challenge belongs to Tranzact IT area specific for System Configuration position.

* There are three branches on the current solution, master is the latest version for all the solution and others branches have been created for deployed on Heroku platform.
  * Deployed Web version: https://premium-calculator-web.herokuapp.com
  * Deployed Rest Api version: https://premium-calculator-api.herokuapp.com

# Challenge description
Below you’ll find a programming challenge, please use C#, .NET Core 3.0 (or greater) for backend and vanilla JavaScript (the only allowed external library is jQuery) for frontend.
We will evaluate your skills in object-oriented, web (HTML) and JavaScript programming.
We expect the code to be production-quality, and can easily be maintained and evolved, not just a barebones algorithm, create public repo on GitHub.com and provide us a link.

## Rest Api

* Build a web service, that receives following parameters:
    *	Date of Birth
    *	State
    *	Age

And provide a premium as a result, based on the following table

![Screen Shot 2021-02-28 at 09 48 06](https://user-images.githubusercontent.com/25852192/109422664-881dfd00-79aa-11eb-8d90-43cfc57736e3.png)

- Represents a criteria wildcard that matches anything within the context of use. I.e. for State * means any valid US state, for MonthOfBirth * means any month.
Age and Date of Birth should be validated that they do match each other. I.e. if DOB is 01.01.1958, value 5 for Age is invalid.
Result should be provided in a JSON format like so:
{
   "premium":"123455.12"
}.

## Web Client

* Build a web site that consumes the web service created in the previous task; result needs to be printed in a textbox in the page, next to it there should be a drop down with frequencies (Monthly, Quarterly, Semi-Annually, Annually) and next to it two textboxes for the calculated values of Annual and Monthly; these values need to be calculated automatically each time the drop down value selected changes. Age control should be auto-populated based on Date of Birth.
  Bonus points: 
  -	Validate fields before calculation. 
  -	Disable controls if value has not been retrieved.
  -	Validate only numeric entries on fields
  -	Handle all possible exception when calculating values
 
 ![Screen Shot 2021-02-28 at 09 59 31](https://user-images.githubusercontent.com/25852192/109422961-aafce100-79ab-11eb-8a76-ed7ab2db9a56.png)
 
* Frequencies meaning:
  *	Monthly: each month
  *	Quarterly: each 3 months
  *	Semi-Annually: each 6 months
  *	Annually: (each 12 months)
- Example:
If url/webservice returns 300.0 and selected frequency is quarterly, that means that 300.0 will be paid every 3 months.
Expected results:
Monthly = 100.0 (300.0 / 3)
Annually = 1200.0 (300.0 * 4)

# Considerations
I consider this product as a first version for the requirement, I will get into more details about what things have been assummed and what others could be better depending on server performance, data volume and security policies.

* There is not defined database technology and I worked with a List structure for storage defined rules for premium values. In case of use Database Technology I have to develop that connection in Infraestructure Tier.
* There is not defined security necessity for consume Rest Api, so I didn't use tokens for Api security. There is strongly recommend to use that depending on security policies and purpose for the Application.
* There is not defined rules for exception or showing error or validation messages.
* The Api is handling exceptions in case of functional errors o defined rules.
* For Age there is not defined rule for maximum age, I consider one hundred years old as a highest possibility.
* There is not defined rule for US State length, I take into consideration two letter characther length.
* There is not defined length of decimals calculating Annuall and Monthly amounts.
* There is not defined security Login or rules for Web client.
* There is not defined screen definition for web client but it is responsive using bootstrap css library.
* There is not date format defined but I using "MM/dd/yyyy" for Api. Web client is taking OS format defined. There won't be any problem because I validate it internally.
