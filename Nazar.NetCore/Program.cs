using Nazar;
using StereoKit;
using System;

App app = new App();

app.PreInit();

if (!SK.Initialize(app.Settings))
    Environment.Exit(1);

app.Init();

// Core application loop
SK.Run(() =>
{
    app.Step();
});