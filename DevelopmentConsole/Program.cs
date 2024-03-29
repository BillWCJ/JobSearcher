﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Runtime.InteropServices;
using Business.DataBaseSeeder;
using Business.Manager;
using Data.EF.JseDb;
using Data.IO.Local;
using Model.Definition;
using Model.Entities.JobMine;

namespace DevelopmentConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            RateMyCoopJobSeeder.SeedDb(Console.WriteLine);
            Console.ReadKey();
        }
    }
}