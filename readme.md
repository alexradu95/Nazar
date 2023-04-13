#Nazar #Stereokit #SceneGraph 

Nazar is a Mixed Reality framework for building multi-agent systems. 
It is designed to be used in a variety of domains, including games, simulations, and training. It is built on top of StereoKit Framework. 
It is designed to be easy to use and extend, and to be able to run on a variety of platforms, including Windows, Linux, and Android.

Right now the framework works using the following modules:

# SceneGraph Module
[RonenNess/MonoGame-SceneGraph: Nodes, Culling & Entities for basic Scene Graphs in MonoGame. (github.com)](https://github.com/RonenNess/MonoGame-SceneGraph)

This module implements basic Nodes and Transformations to implement a Scene Graph in StereoKit.

## What's a scene graph?

A scene graph is a collection of nodes in a graph or tree structure. A tree node (in the overall tree structure of the scene graph) may have many children but often only a single parent, with the effect of a parent applied to all its child nodes; an operation performed on a group automatically propagates its effect to all of its members. In many programs, associating a geometrical transformation matrix (see also transformation and matrix) at each group level and concatenating such matrices together is an efficient and natural way to process such operations. A common feature, for instance, is the ability to group related shapes/objects into a compound object that can then be moved, transformed, selected, etc. as easily as a single object.

Scene graphs are useful for modern games using 3D graphics and increasingly large worlds or levels. In such applications, nodes in a scene graph (generally) represent entities or objects in the scene. For instance, a game might define a logical relationship between a knight and a horse so that the knight is considered an extension to the horse. The scene graph would have a 'horse' node with a 'knight' node attached to it. As well as describing the logical relationship, the scene graph may also describe the spatial relationship of the various entities: the knight moves through 3D space as the horse moves.

## Main objects

This lib contains 4 main classes you should know:

## Node

A basic scene node with transformation. To build your scene create nodes and child-nodes and set their transformations.
Note: a Node does not have a graphic representation, eg it does not render anything. A node only handles the hierarchy and transformation and hold entities that render stuff.

## Entity
An entity is a renderable object you attach to nodes. For example, the most basic Entity is a ModelEntity, which renders a 3D model loaded by the content manager.

Remember: Entities handle rendering, Nodes handle transformations.

## Transformations
A set of transformations that a Node can have + helper functions to build a matrix. 

# PubSubHub Module
[upta/pubsub: An extremely light-weight, easy to use PCL pub/sub library (github.com)](https://github.com/upta/pubsub)

In general, the publish/subscribe pattern is great when you want to communicate between pieces of your app without having those pieces being tightly dependent on each other. You might find [this article on the subject](http://blog.mgechev.com/2013/04/24/why-to-use-publishsubscribe-in-javascript/) interesting (it talks specifically about javascript, but the concept applies)

### Some explanation

To keep things simple, yet flexible, PubSubHub Module is implemented using core ideas:

-   Different kinds of messages are delineated by CLR type.
    -   This avoids the need to have a list of string constants (or just magic strings), enums, whatever to define what you want to listen for/send.
    -   This gives us nice strongly-typed data that can be passed from our Publish methods to our Subscribe handlers (i.e. Product above)

### How to use it

```C#
public class Page
{
    Hub hub = Hub.Default;
	
	public Page()
	{
		hub.Subscribe<Product>(this, product =>
		{
			// highly interesting things
		});
	}
}

//  Tell others that interesting things happened.

public class OtherPage
{
    Hub hub = Hub.Default;
	
	public void ProductPurchased()
	{
		hub.Publish( new Product() );
	}
}

// Stop listening when you don't care anymore.

public class Page
{
	Hub hub = Hub.Default;
	
	public void WereDoneHere()
	{
		hub.Unsubscribe<Product>();
	}
}

```


# Stereokit Steppers and our custom StepperManager

> How do the Initialize, Update, Shutdown methods get called. I have an Interface IRun (just like SK's Demo ITest). I derive my class from IRun and implement a simple cube in the Update method. What is the mechanism that calls Update?

The only event you normally get from StereoKit is the main Step loop! Every single frame this gets called, and everything else is just derived from that. 

```c#

App app = new();

app.PreInit();
SK.Initialize(...);
app.Post(Init);

SK.Run(()=>{
    App.Step()
});

App.Shutdown();

```


The `IStepper` is exclusive to the C# API of StereoKit, and we have this around largely so that it's easy to build and provide tools with a single attachment point! These steppers can be added to your project and forgotten about with a simple `SK.AddStepper<RenderCamera>()` sort of line. It's part of StereoKit's core, so you can't extend it or add additional control over its lifecycle. It also has a lot more considerations for "what if a developer did this strange thing?", like for example, adding an `IStepper` from a separate thread before initialization. So it's a pretty robust system with a bit more weight and complexity. 

```c#

﻿namespace StereoKit.Framework
{
	/// <summary>This is a lightweight standard interface for fire-and-forget
	/// systems that can be attached to StereoKit! This is particularly handy
	/// for extensions/plugins that need to run in the background of your
	/// application, or even for managing some of your own simpler systems.
	/// 
	/// `IStepper`s can be added before or after the call to `SK.Initialize`,
	/// and this does affect when the `IStepper.Initialize` call will be made!
	/// `IStepper.Initialize` is always called _after_ `SK.Initialize`. This
	/// can be important to note when writing code that uses SK functions that
	/// are dependant on initialization, you'll want to avoid putting this code
	/// in the constructor, and add them to `Initialize` instead.
	/// 
	/// `IStepper`s also pay attention to threading! `Initialize` and `Step`
	/// always happen on the main thread, even if the constructor is called on
	/// a different one.</summary>
	public interface IStepper
	{
		/// <summary>Is this IStepper enabled? When false, StereoKit will not
		/// call Step. This can be a good way to temporarily disable the
		/// IStepper without removing or shutting it down.</summary>
		bool Enabled { get; }

		/// <summary>This is called by StereoKit at the start of the next
		/// frame, and on the main thread. This happens before StereoKit's main
		/// `Step` callback, and always after `SK.Initialize`.</summary>
		/// <returns>If false is returned here, this `IStepper` will be
		/// immediately removed from the list of `IStepper`s, and neither
		/// `Step` nor `Shutdown` will be called.</returns>
		bool Initialize();

		/// <summary>This Step method will be called every frame of the
		/// application, as long as `Enabled` is `true`. This happens
		/// immediately before the main application's `Step` callback.
		/// </summary>
		void Step();

		/// <summary>This is called when the `IStepper` is removed, or the
		/// application shuts down. This is always called on the main thread,
		/// and happens at the start of the next frame, before the main
		/// application's `Step` callback.</summary>
		void Shutdown();
	}
}

```

`IStepper` is a nice thing to use, but I like to think of an `IStepper` as a system, rather than an object. You could treat it like an object if you wanted to, but I suspect you'd find it somewhat limited in functionality there. It's also somewhat object oriented, so it comes with the performance overhead of using classes by reference and virtual methods.

Inside the Step Function of the application more precislely in `App.Step()` the core of the Nazar application happens.
Where we use the SceneGraphManager to manage Nodes in 3D with their children and their behaviorus.

//TODO -> Think of an idea on how to use the of autonomous agent for each module

This is an example:  

```c#

public class NazarApp
{
	// The PubSubHubModule
    public static MessagingHub MessagingHub = MessagingHub.Default;
    // The StepperManager, which handles all the Stereokit Steppers
    public IStepperManager StepperManager => new StepperManager();
    // The creation objects of the application
    public ISceneGraph SceneGraphManager => new SceneGraphManager();

    // Called before SK.Initialize is triggered
    public void PreInit()
    {
        StepperManager.RegisterStepper<PassthroughFBExt>();
        StepperManager.RegisterStepper<RenderCamera>();
    }

    // Called after SK.Initialize is triggered
    public void Init()
    {
        StepperManager.RegisterStepper(HandMenuGenerator.BuildHandMenu());
    }

    // This Step method will be called every frame of the application
    public void Step()
    {
    }
}

```
