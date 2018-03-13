// Testing of determination of day of the week functions used in an Arduino sketch
using System;

public class StartUp
{
    public static void Main()
    {
        Console.WriteLine("Enter an year, to determine which day of week January 1st falls on:");
        int year = int.Parse(Console.ReadLine());

        int dayOfWeekGauss = (1 + (5 * ((year - 1) % 4)) + (4 * ((year - 1) % 100)) + (6 * ((year - 1) % 400))) % 7;

        int dayOfWeek = (year * 365 + ((year - 1) / 4) - ((year - 1) / 100) + ((year - 1) / 400)) % 7;

        Console.Write("From stack overflow: ");
        Console.WriteLine(dayOfWeek);

        Console.Write("Gauss: ");
        Console.WriteLine(dayOfWeekGauss);

        // day of week of 1st Jan = R(1 + 5R(A - 1, 4) + 4R(A - 1, 100) + 6R(A - 1, 400), 7)
        // R(y, m) = y % m
        // A - given year;

        // Second try from stack overflow
        //(year * 365 + trunc((year - 1) / 4) - trunc((year - 1) / 100) +
        //   trunc((year - 1) / 400)) % 7

        Console.WriteLine("Enter month:");
        int month = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter day");
        int day = int.Parse(Console.ReadLine());

        byte result = ReturnDayOfWeek(year, month, day);

        Console.Write("Day of week: ");
        string[] daysOfWeek = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };


        Console.WriteLine(daysOfWeek[result]);


    }

    static byte ReturnDayOfWeek(int givenYear, int givenMonth, int givenDay)
    {
        byte[] monthLengths = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        bool leapYear = IsLeapYear(givenYear);
        int result = WhatDayIsFirstJan(givenYear);

        for (byte i = 0; i < givenMonth; i++)
        {
            result += monthLengths[i];
        }

        result += givenDay - 1;

        if (leapYear && givenMonth > 1)
        {
                result++;
        }

        result %= 7;
        return (byte)result;
    }

    static bool IsLeapYear(int year)
    {
        bool result = false;

        bool yearCanBeDividedBy4 = year % 4 == 0;
        bool yearCanBeDividedBy100 = year % 100 == 0;
        bool yearCanBeDividedBy400 = year % 400 == 0;

        if (yearCanBeDividedBy400)
        {
            result = true;
        }
        else if (yearCanBeDividedBy100)
        {
            result = false;
        }
        else if (yearCanBeDividedBy4)
        {
            result = true;
        }
        else
        {
            result = false;
        }

        return result;
    }

    static byte WhatDayIsFirstJan(int givenYear)
    {
        ulong year = (ulong)givenYear;
        byte day = (byte)((year * 365 + ((year - 1) / 4) - ((year - 1) / 100) + ((year - 1) / 400)) % 7);

        return day;
    }
}