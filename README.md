# TaxiCabWeb

### Taxi Cab Fare Calculator - will calculate the fare for a taxi ride in NYC based on a few rules.

**Backend** written in ASP.NET MVC 5, all projects are .Net Framework 4.6.1

**Frontend** written in AngularJS 1.5.6 (\TaxiCabWeb\TaxiCabWeb\Scripts\app\)

**JS Tests** using jasmine 2.99.0 and karma 3.1.3 (\TaxiCabWeb\TaxiCabWeb\Scripts\app\test.js)

  To run tests `cd .\TaxiCabWeb\TaxiCabWeb` and enter `npm test`
  
There is a weird error thrown likely due to a problem during installation:
```
    "message": "An error was thrown in afterAll\nUncaught ReferenceError: require is not defined",
    "str": "An error was thrown in afterAll\nUncaught ReferenceError: require is not defined"
```
I added a failing test so you can see the tests above it are running and passing.
```
    it("should fail so you know tests are actually running", function () {
        expect("this to fail").toBe("see, it fails");
    });
```
