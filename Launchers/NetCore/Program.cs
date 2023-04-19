using Nazar;
using StereoKit;
using System;

App app = new();

app.RegisterPreInitModules();

if (!SK.Initialize(app.Settings))
    Environment.Exit(1);

app.RegisterPostInitModules();

// Core application loop
SK.Run(() =>
{
    app.Step();
});