```C#
// Hiker.cs
public class Hiker
{
    public static bool IsLeapYear(int year)
    {
        if(year % 100 == 0 && year % 400 != 0)
            return false;
        return year % 4 == 0;
    }
}
```

```c#
// HikerTest.cs
using NUnit.Framework;

[TestFixture]
public class HikerTest
{
    /*
        2001 is a typical common year and 
        1996 is a typical leap year, whereas 
        1900 is an atypical common year and 
        2000 is an atypical leap year.
    */
    [Test]
    public void typical_common_year()
    {
        Assert.AreEqual(false, Hiker.IsLeapYear(2001));
    }
    
    [Test]
    public void typical_leap_year()
    {
        Assert.AreEqual(true, Hiker.IsLeapYear(1996));
    }
    
    [Test]
    public void atypical_common_year()
    {
        Assert.AreEqual(false, Hiker.IsLeapYear(1900));
    }
    
    [Test]
    public void atypical_leap_year()
    {
        Assert.AreEqual(true, Hiker.IsLeapYear(2000));
    }
}
```