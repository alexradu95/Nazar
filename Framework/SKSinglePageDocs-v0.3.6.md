
# class Font

This class represents a text font asset! On the back-end, this asset
is composed of a texture with font characters rendered to it, and a list of
data about where, and how large those characters are on the texture.

This asset is used anywhere that text shows up, like in the UI or Text classes!

## Font.Find
```csharp
static Font Find(string fontId)
```
Searches the asset list for a font with the given Id, returning null if
none is found.

|  |  |
|--|--|
|string fontId|Id of the font you're looking for.|
|RETURNS: Font|An existing font asset, or null if none is found.|


## Font.FromFile
```csharp
static Font FromFile(String[] fontFiles)
```
Loads a font and creates a font asset from it.

|  |  |
|--|--|
|String[] fontFiles|A list of file addresses for the font! For             example: 'C:/Windows/Fonts/segoeui.ttf'. If a glyph is not found,             StereoKit will look in the next font file in the list.|
|RETURNS: Font|A font from the given files, or null if all of the files failed to load properly! If any of the given files successfully loads, then this font will be a valid asset.|


## Font.Default

```csharp
static Font Default{ get }
```

The default font used by StereoKit's text. This varies
from platform to platform, but is typically a sans-serif general
purpose font, such as Segoe UI.
# enum MatParamName

A better way to access standard shader parameter names,
instead of using just strings! If you have your own custom
parameters, you can still access them via the string methods, but
this is checked and verified by the compiler!

## MatParamName.DiffuseTex

```csharp
static MatParamName DiffuseTex
```

The primary color texture for the shader! Diffuse,
Albedo, 'The Texture', or whatever you want to call it, this is
usually the base color that the shader works with.

This represents the texture param 'diffuse'.

## MatParamName.EmissionTex

```csharp
static MatParamName EmissionTex
```

This texture is unaffected by lighting, and is
frequently just added in on top of the material's final color!
Tends to look really glowy.

This represents the texture param 'emission'.

## MatParamName.MetalTex

```csharp
static MatParamName MetalTex
```

For physically based shaders, metal is a texture that
encodes metallic and roughness data into the 'B' and 'G'
channels, respectively.

This represents the texture param 'metal'.

## MatParamName.NormalTex

```csharp
static MatParamName NormalTex
```

The 'normal map' texture for the material! This texture
contains information about the direction of the material's
surface, which is used to calculate lighting, and make surfaces
look like they have more detail than they actually do. Normals
are in Tangent Coordinate Space, and the RGB values map to XYZ
values.

This represents the texture param 'normal'.

## MatParamName.OcclusionTex

```csharp
static MatParamName OcclusionTex
```

Used by physically based shaders, this can be used for
baked ambient occlusion lighting, or to remove specular
reflections from areas that are surrounded by geometry that would
likely block reflections.

This represents the texture param 'occlusion'.

## MatParamName.ColorTint

```csharp
static MatParamName ColorTint
```

A per-material color tint, behavior could vary from
shader to shader, but often this is just multiplied against the
diffuse texture right at the start.

This represents the Color param 'color'.

## MatParamName.EmissionFactor

```csharp
static MatParamName EmissionFactor
```

A multiplier for emission values sampled from the emission
texture. The default emission texture in SK shaders is white, and
the default value for this parameter is 0,0,0,0.

This represents the Color param 'emission_factor'.

## MatParamName.MetallicAmount

```csharp
static MatParamName MetallicAmount
```

For physically based shader, this is a multiplier to
scale the metallic properties of the material.

This represents the float param 'metallic'.

## MatParamName.RoughnessAmount

```csharp
static MatParamName RoughnessAmount
```

For physically based shader, this is a multiplier to
scale the roughness properties of the material.

This represents the float param 'roughness'.

## MatParamName.TexScale

```csharp
static MatParamName TexScale
```

Not necessarily present in all shaders, this multiplies
the UV coordinates of the mesh, so that the texture will repeat.
This is great for tiling textures!

This represents the float param 'tex_scale'.

## MatParamName.ClipCutoff

```csharp
static MatParamName ClipCutoff
```

In clip shaders, this is the cutoff value below which
pixels are discarded. Typically, the diffuse/albedo's alpha
component is sampled for comparison here.

This represents the float param 'cutoff'.
# struct MatParamInfo

Information and details about the shader parameters
available on a Material. Currently only the text name and data type
of the parameter are surfaced here. This contains textures as well as
global constant buffer variables.

## MatParamInfo.name

```csharp
string name
```

The name of the shader parameter, as provided inside of
the shader file itself. These are the 'global' variables defined
in the shader, as well as textures. The shader compiler will
output a list of parameter names when compiling, so check the
output window after building if you're uncertain what you'll
find.

See `MatParamName` for a list of "standardized" parameter names.

## MatParamInfo.type

```csharp
MaterialParam type
```

This is the data type that StereoKit recognizes the
parameter to be.
# class Material

A Material describes the surface of anything drawn on the
graphics card! It is typically composed of a Shader, and shader
properties like colors, textures, transparency info, etc.

Items drawn with the same Material can be batched together into a
single, fast operation on the graphics card, so re-using materials
can be extremely beneficial for performance!

## Material.Transparency

```csharp
Transparency Transparency{ get set }
```

What type of transparency does this Material use?
Default is None. Transparency has an impact on performance, and
draw order. Check the Transparency enum for details.

## Material.FaceCull

```csharp
Cull FaceCull{ get set }
```

How should this material cull faces?

## Material.Wireframe

```csharp
bool Wireframe{ get set }
```

Should this material draw only the edges/wires of the
mesh? This can be useful for debugging, and even some kinds of
visualization work. Note that this may not work on some mobile
OpenGL systems like Quest.

## Material.DepthTest

```csharp
DepthTest DepthTest{ get set }
```

How does this material interact with the ZBuffer?
Generally DepthTest.Less would be normal behavior: don't draw
objects that are occluded. But this can also be used to achieve
some interesting effects, like you could use DepthTest.Greater
to draw a glow that indicates an object is behind something.

## Material.DepthWrite

```csharp
bool DepthWrite{ get set }
```

Should this material write to the ZBuffer? For opaque
objects, this generally should be true. But transparent objects
writing to the ZBuffer can be problematic and cause draw order
issues. Note that turning this off can mean that this material
won't get properly accounted for when the MR system is performing
late stage reprojection.

Not writing to the buffer can also be faster! :)

## Material.QueueOffset

```csharp
int QueueOffset{ get set }
```

This property will force this material to draw earlier
or later in the draw queue. Positive values make it draw later,
negative makes it earlier. This can be helpful for tweaking
performance! If you know an object is always going to be close to
the user and likely to obscure lots of objects (like hands),
drawing it earlier can mean objects behind it get discarded much
faster! Similarly, objects that are far away (skybox!) can be
pushed towards the back of the queue, so they're more likely to
be discarded early.

## Material.ParamCount

```csharp
int ParamCount{ get }
```

The number of shader parameters available to this
material, includes global shader variables as well as textures.

## Material.Shader

```csharp
Shader Shader{ get set }
```

Gets a link to the Shader that the Material is currently
using, or overrides the Shader this material uses.

## Material.Material
```csharp
void Material(Shader shader)
```
Creates a material from a shader, and uses the shader's
default settings. Uses an auto-generated id.

|  |  |
|--|--|
|Shader shader|Any valid shader.|
```csharp
void Material(string id, Shader shader)
```
Creates a material from a shader, and uses the shader's
default settings.

|  |  |
|--|--|
|string id|Set the material's id to this.|
|Shader shader|Any valid shader.|


## Material.GetAllParamInfo
```csharp
IEnumerable`1 GetAllParamInfo()
```
Gets an enumerable list of all parameter information on
the Material. This includes all global shader variables and
textures.

|  |  |
|--|--|
|RETURNS: IEnumerable`1|A pretty standard IEnumarable of MatParamInfo that can be used with foreach.|


## Material.GetParamInfo
```csharp
MatParamInfo GetParamInfo(int index)
```
Gets available shader parameter information for the
parameter at the indicated index. Parameters are listed as
variables first, then textures.

|  |  |
|--|--|
|int index|Index of the shader parameter, bounded by             ParamCount.|
|RETURNS: MatParamInfo|A structure that contains all the available information about the parameter.|


## Material.Copy
```csharp
Material Copy()
```
Creates a new Material asset with the same shader and
properties! Draw calls with the new Material will not batch
together with this one.

|  |  |
|--|--|
|RETURNS: Material|A new Material asset with the same shader and properties.|
```csharp
static Material Copy(string materialId)
```
Creates a new Material asset with the same shader and
properties! Draw calls with the new Material will not batch
together with the source Material.

|  |  |
|--|--|
|string materialId|Which Material are you looking for?|
|RETURNS: Material|A new Material asset with the same shader and properties. Returns null if no materials are found with the given id.|


## Material.SetFloat
```csharp
void SetFloat(string name, float value)
```
Sets a shader parameter with the given name to the
provided value. If no parameter is found, nothing happens, and
the value is not set!

|  |  |
|--|--|
|string name|Name of the shader parameter.|
|float value|New value for the parameter.|


## Material.SetColor
```csharp
void SetColor(string name, Color32 colorGamma)
```
Sets a shader parameter with the given name to the
provided value. If no parameter is found, nothing happens, and
the value is not set!

|  |  |
|--|--|
|string name|Name of the shader parameter.|
|Color32 colorGamma|The gamma space color for the shader             to use.|
```csharp
void SetColor(string name, Color colorGamma)
```
Sets a shader parameter with the given name to the
provided value. If no parameter is found, nothing happens, and
the value is not set!

|  |  |
|--|--|
|string name|Name of the shader parameter.|
|Color colorGamma|The gamma space color for the shader             to use.|


## Material.SetVector
```csharp
void SetVector(string name, Vec4 value)
```
Sets a shader parameter with the given name to the
provided value. If no parameter is found, nothing happens, and
the value is not set!

|  |  |
|--|--|
|string name|Name of the shader parameter.|
|Vec4 value|New value for the parameter.|
```csharp
void SetVector(string name, Vec3 value)
```
Sets a shader parameter with the given name to the
provided value. If no parameter is found, nothing happens, and
the value is not set!

|  |  |
|--|--|
|string name|Name of the shader parameter.|
|Vec3 value|New value for the parameter.|
```csharp
void SetVector(string name, Vec2 value)
```
Sets a shader parameter with the given name to the
provided value. If no parameter is found, nothing happens, and
the value is not set!

|  |  |
|--|--|
|string name|Name of the shader parameter.|
|Vec2 value|New value for the parameter.|


## Material.SetInt
```csharp
void SetInt(string name, int value)
```
Sets a shader parameter with the given name to the
provided value. If no parameter is found, nothing happens, and
the value is not set!

|  |  |
|--|--|
|string name|Name of the shader parameter.|
|int value|New value for the parameter.|
```csharp
void SetInt(string name, int value1, int value2)
```
Sets a shader parameter with the given name to the
provided value. If no parameter is found, nothing happens, and
the value is not set!

|  |  |
|--|--|
|string name|Name of the shader parameter.|
|int value1|New value for the parameter.|
|int value2|New value for the parameter.|
```csharp
void SetInt(string name, int value1, int value2, int value3)
```
Sets a shader parameter with the given name to the
provided value. If no parameter is found, nothing happens, and
the value is not set!

|  |  |
|--|--|
|string name|Name of the shader parameter.|
|int value1|New value for the parameter.|
|int value2|New value for the parameter.|
|int value3|New value for the parameter.|
```csharp
void SetInt(string name, int value1, int value2, int value3, int value4)
```
Sets a shader parameter with the given name to the
provided value. If no parameter is found, nothing happens, and
the value is not set!

|  |  |
|--|--|
|string name|Name of the shader parameter.|
|int value1|New value for the parameter.|
|int value2|New value for the parameter.|
|int value3|New value for the parameter.|
|int value4|New value for the parameter.|


## Material.SetUInt
```csharp
void SetUInt(string name, uint value)
```
Sets a shader parameter with the given name to the
provided value. If no parameter is found, nothing happens, and
the value is not set!

|  |  |
|--|--|
|string name|Name of the shader parameter.|
|uint value|New value for the parameter.|
```csharp
void SetUInt(string name, uint value1, uint value2)
```
Sets a shader parameter with the given name to the
provided value. If no parameter is found, nothing happens, and
the value is not set!

|  |  |
|--|--|
|string name|Name of the shader parameter.|
|uint value1|New value for the parameter.|
|uint value2|New value for the parameter.|
```csharp
void SetUInt(string name, uint value1, uint value2, uint value3)
```
Sets a shader parameter with the given name to the
provided value. If no parameter is found, nothing happens, and
the value is not set!

|  |  |
|--|--|
|string name|Name of the shader parameter.|
|uint value1|New value for the parameter.|
|uint value2|New value for the parameter.|
|uint value3|New value for the parameter.|
```csharp
void SetUInt(string name, uint value1, uint value2, uint value3, uint value4)
```
Sets a shader parameter with the given name to the
provided value. If no parameter is found, nothing happens, and
the value is not set!

|  |  |
|--|--|
|string name|Name of the shader parameter.|
|uint value1|New value for the parameter.|
|uint value2|New value for the parameter.|
|uint value3|New value for the parameter.|
|uint value4|New value for the parameter.|


## Material.SetBool
```csharp
void SetBool(string name, bool value)
```
Sets a shader parameter with the given name to the
provided value. If no parameter is found, nothing happens, and
the value is not set!

|  |  |
|--|--|
|string name|Name of the shader parameter.|
|bool value|New value for the parameter.|


## Material.SetMatrix
```csharp
void SetMatrix(string name, Matrix value)
```
Sets a shader parameter with the given name to the
provided value. If no parameter is found, nothing happens, and
the value is not set!

|  |  |
|--|--|
|string name|Name of the shader parameter.|
|Matrix value|New value for the parameter.|


## Material.SetTexture
```csharp
void SetTexture(string name, Tex value)
```
Sets a shader parameter with the given name to the
provided value. If no parameter is found, nothing happens, and
the value is not set!

|  |  |
|--|--|
|string name|Name of the shader parameter.|
|Tex value|New value for the parameter.|


## Material.SetData
```csharp
void SetData(string name, T& serializableData)
```
This allows you to set more complex shader data types such
as structs. Note the SK doesn't guard against setting data of the
wrong size here, so pay extra attention to the size of your data
here, and ensure it matched up with the shader!

|  |  |
|--|--|
|string name|Name of the shader parameter.|
|T& serializableData|New value for the parameter.|


## Material.Find
```csharp
static Material Find(string materialId)
```
Looks for a Material asset that's already loaded,
matching the given id!

|  |  |
|--|--|
|string materialId|Which Material are you looking for?|
|RETURNS: Material|A link to the Material matching 'id', null if none is found.|


## Material.Default

```csharp
static Material Default{ get }
```

The default material! This is used by many models and
meshes rendered from within StereoKit. Its shader is tuned for
high performance, and may change based on system performance
characteristics, so it can be great to copy this one when
creating your own materials! Or if you want to override
StereoKit's default material, here's where you do it!

## Material.PBR

```csharp
static Material PBR{ get }
```

The default Physically Based Rendering material! This is
used by StereoKit anytime a mesh or model has metallic or
roughness properties, or needs to look more realistic. Its shader
may change based on system performance characteristics, so it can
be great to copy this one when creating your own materials! Or if
you want to override StereoKit's default PBR behavior, here's
where you do it! Note that the shader used by default here is
much more costly than Default.Material.

## Material.PBRClip

```csharp
static Material PBRClip{ get }
```

Same as MaterialPBR, but it uses a discard clip for
transparency.

## Material.UI

```csharp
static Material UI{ get }
```

The material used by the UI! By default, it uses a shader
that creates a 'finger shadow' that shows how close the finger is
to the UI.

## Material.UIBox

```csharp
static Material UIBox{ get }
```

A material for indicating interaction volumes! It
renders a border around the edges of the UV coordinates that will
'grow' on proximity to the user's finger. It will discard pixels
outside of that border, but will also show the finger shadow.
This is meant to be an opaque material, so it works well for
depth LSR.

This material works best on cube-like meshes where each face has
UV coordinates from 0-1.

Shader Parameters:
```color                - color
border_size          - meters
border_size_grow     - meters
border_affect_radius - meters```

## Material.UIQuadrant

```csharp
static Material UIQuadrant{ get }
```

The material used by the UI for Quadrant Sized UI
elements. See UI.QuadrantSizeMesh for additional details. By
default, it uses a shader that creates a 'finger shadow' that shows
how close the finger is to the UI.

## Material.Unlit

```csharp
static Material Unlit{ get }
```

The default unlit material! This is used by StereoKit
any time a mesh or model needs to be rendered with an unlit
surface. Its shader may change based on system performance
characteristics, so it can be great to copy this one when
creating your own materials! Or if you want to override
StereoKit's default unlit behavior, here's where you do it!

## Material.UnlitClip

```csharp
static Material UnlitClip{ get }
```

The default unlit material with alpha clipping! This is
used by StereoKit for unlit content with transparency, where
completely transparent pixels are discarded. This means less
alpha blending, and fewer visible alpha blending issues! In
particular, this is how Sprites are drawn. Its shader may change
based on system performance characteristics, so it can be great
to copy this one when creating your own materials! Or if you want
to override StereoKit's default unlit clipped behavior, here's
where you do it!
# class MaterialBuffer

This is a chunk of memory that will get bound to all shaders
at a particular register slot. StereoKit uses this to provide engine
values to the shader, and you can also use this to drive graphical
shader systems of your own!

For example, if your application has a custom lighting system, fog,
wind, or some other system that multiple shaders might need to refer
to, this is the perfect tool to use.

The type 'T' for this buffer must be a struct that uses the
`[StructLayout(LayoutKind.Sequential)]` attribute for proper copying.
It should also match the layout of your equivalent cbuffer in the
shader file. Note that shaders often have [specific byte alignment](https://docs.microsoft.com/en-us/windows/win32/direct3dhlsl/dx-graphics-hlsl-packing-rules)
requirements! Example:

C#
```csharp
[StructLayout(LayoutKind.Sequential)]
struct BufferData {
Vec3  windDirection;
float windStrength
}
```

HLSL
```c
cbuffer BufferData : register(b3) {
float3 windDirection;
float  windStrength;
}
```

## MaterialBuffer.MaterialBuffer
```csharp
void MaterialBuffer(int registerSlot)
```
Create a new global MaterialBuffer bound to the register
slot id. All shaders will have access to the data provided via
this instance's `Set`.

|  |  |
|--|--|
|int registerSlot|Valid values are 3-16. This is the              register id that this data will be bound to. In HLSL, you'll see             the slot id for '3' indicated like this `: register(b3)`|


## MaterialBuffer.Set
```csharp
void Set(T& data)
```
This will upload your data to the GPU for shaders to use.

|  |  |
|--|--|
|T& data|The data you wish to upload to the GPU.|

# class Mesh

A Mesh is a single collection of triangular faces with extra surface
information to enhance rendering! StereoKit meshes are composed of a
list of vertices, and a list of indices to connect the vertices into
faces. Nothing more than that is stored here, so typically meshes are
combined with Materials, or added to Models in order to draw them.

Mesh vertices are composed of a position, a normal (direction of the
vert), a uv coordinate (for mapping a texture to the mesh's surface),
and a 32 bit color containing red, green, blue, and alpha
(transparency).

Mesh indices are stored as unsigned ints, so you can have a mesh with
a fudgeton of verts! 4 billion or so :)

## Mesh.Bounds

```csharp
Bounds Bounds{ get set }
```

This is a bounding box that encapsulates the Mesh! It's
used for collision, visibility testing, UI layout, and probably
other things. While it's normally calculated from the mesh vertices,
you can also override this to suit your needs.

## Mesh.KeepData

```csharp
bool KeepData{ get set }
```

Should StereoKit keep the mesh data on the CPU for later
access, or collision detection? Defaults to true. If you set this
to false before setting data, the data won't be stored. If you
call this after setting data, that stored data will be freed! If
you set this to true again later on, it will not contain data
until it's set again.

## Mesh.VertCount

```csharp
int VertCount{ get }
```

The number of vertices stored in this Mesh! This is
available to you regardless of whether or not KeepData is set.

## Mesh.IndCount

```csharp
int IndCount{ get }
```

The number of indices stored in this Mesh! This is
available to you regardless of whether or not KeepData is set.

## Mesh.Mesh
```csharp
void Mesh()
```
Creates an empty Mesh asset. Use SetVerts and SetInds to
add data to it!


## Mesh.SetVerts
```csharp
void SetVerts(Vertex[] verts)
```
Assigns the vertices for this Mesh! This will create a
vertex buffer object on the graphics card right away. If you're
calling this a second time, the buffer will be marked as dynamic
and re-allocated. If you're calling this a third time, the buffer
will only re-allocate if the buffer is too small, otherwise it
just copies in the data!

|  |  |
|--|--|
|Vertex[] verts|An array of vertices to add to the mesh.             Remember to set all the relevant values! Your material will often             show black if the Normals or Colors are left at their default             values.|


## Mesh.GetVerts
```csharp
Vertex[] GetVerts()
```
This marshalls the Mesh's vertex data into an array. If
KeepData is false, then the Mesh is _not_ storing verts on the CPU,
and this information will _not_ be available.

Due to the way marshalling works, this is _not_ a cheap function!

|  |  |
|--|--|
|RETURNS: Vertex[]|An array of vertices representing the Mesh, or null if KeepData is false.|


## Mesh.SetInds
```csharp
void SetInds(UInt32[] inds)
```
Assigns the face indices for this Mesh! Faces are always
triangles, there are only ever three indices per face. This
function will create a index buffer object on the graphics card
right away. If you're calling this a second time, the buffer will
be marked as dynamic and re-allocated. If you're calling this a
third time, the buffer will only re-allocate if the buffer is too
small, otherwise it just copies in the data!

|  |  |
|--|--|
|UInt32[] inds|A list of face indices, must be a multiple of             3. Each index represents a vertex from the array assigned using             SetVerts.|


## Mesh.GetInds
```csharp
UInt32[] GetInds()
```
This marshalls the Mesh's index data into an array. If
KeepData is false, then the Mesh is _not_ storing indices on the
CPU, and this information will _not_ be available.

Due to the way marshalling works, this is _not_ a cheap function!

|  |  |
|--|--|
|RETURNS: UInt32[]|An array of indices representing the Mesh, or null if KeepData is false.|


## Mesh.Intersect
```csharp
bool Intersect(Ray modelSpaceRay, Ray& modelSpaceAt)
```
Checks the intersection point of this ray and a Mesh
with collision data stored on the CPU. A mesh without collision
data will always return false. Ray must be in model space,
intersection point will be in model space too. You can use the
inverse of the mesh's world transform matrix to bring the ray
into model space, see the example in the docs!

|  |  |
|--|--|
|Ray modelSpaceRay|Ray must be in model space, the             intersection point will be in model space too. You can use the             inverse of the mesh's world transform matrix to bring the ray             into model space, see the example in the docs!|
|Ray& modelSpaceAt|The intersection point and surface             direction of the ray and the mesh, if an intersection occurs.             This is in model space, and must be transformed back into world             space later. Direction is not guaranteed to be normalized,              especially if your own model->world transform contains scale/skew             in it.|
|RETURNS: bool|True if an intersection occurs, false otherwise!|
```csharp
bool Intersect(Ray modelSpaceRay, Ray& modelSpaceAt, UInt32& outStartInds)
```
Checks the intersection point of this ray and a Mesh
with collision data stored on the CPU. A mesh without collision
data will always return false. Ray must be in model space,
intersection point will be in model space too. You can use the
inverse of the mesh's world transform matrix to bring the ray
into model space, see the example in the docs!

|  |  |
|--|--|
|Ray modelSpaceRay|Ray must be in model space, the             intersection point will be in model space too. You can use the             inverse of the mesh's world transform matrix to bring the ray             into model space, see the example in the docs!|
|Ray& modelSpaceAt|The intersection point and surface             direction of the ray and the mesh, if an intersection occurs.             This is in model space, and must be transformed back into world             space later. Direction is not guaranteed to be normalized,              especially if your own model->world transform contains scale/skew             in it.|
|UInt32& outStartInds|The index of the first index of the triangle that was hit|
|RETURNS: bool|True if an intersection occurs, false otherwise!|


## Mesh.GetTriangle
```csharp
bool GetTriangle(uint triangleIndex, Vertex& a, Vertex& b, Vertex& c)
```
Retrieves the vertices associated with a particular
triangle on the Mesh.

|  |  |
|--|--|
|uint triangleIndex|Starting index of the triangle, should             be a multiple of 3.|
|Vertex& a|The first vertex of the found triangle|
|Vertex& b|The second vertex of the found triangle|
|Vertex& c|The third vertex of the found triangle|
|RETURNS: bool|Returns true if triangle index was valid|


## Mesh.Draw
```csharp
void Draw(Material material, Matrix transform, Color colorLinear, RenderLayer layer)
```
Adds a mesh to the render queue for this frame! If the
Hierarchy has a transform on it, that transform is combined with
the Matrix provided here.

|  |  |
|--|--|
|Material material|A Material to apply to the Mesh.|
|Matrix transform|A Matrix that will transform the mesh              from Model Space into the current Hierarchy Space.|
|Color colorLinear|A per-instance linear space color value             to pass into the shader! Normally this gets used like a material             tint. If you're  adventurous and don't need per-instance colors,             this is a great spot to pack in extra per-instance data for the             shader!|
|RenderLayer layer|All visuals are rendered using a layer              bit-flag. By default, all layers are rendered, but this can be              useful for filtering out objects for different rendering              purposes! For example: rendering a mesh over the user's head from             a 3rd person perspective, but filtering it out from the 1st             person perspective.|
```csharp
void Draw(Material material, Matrix transform)
```
Adds a mesh to the render queue for this frame! If the
Hierarchy has a transform on it, that transform is combined with
the Matrix provided here.

|  |  |
|--|--|
|Material material|A Material to apply to the Mesh.|
|Matrix transform|A Matrix that will transform the mesh              from Model Space into the current Hierarchy Space.|


## Mesh.GeneratePlane
```csharp
static Mesh GeneratePlane(Vec2 dimensions, int subdivisions)
```
Generates a plane on the XZ axis facing up that is
optionally subdivided, pre-sized to the given dimensions. UV
coordinates start at 0,0 at the -X,-Z corer, and go to 1,1 at the
+X,+Z corner!

NOTE: This generates a completely new Mesh asset on the GPU, and
is best done during 'initialization' of your app/scene. You may
also be interested in using the pre-generated `Mesh.Quad` asset
if it already meets your needs.

|  |  |
|--|--|
|Vec2 dimensions|How large is this plane on the XZ axis,             in meters?|
|int subdivisions|Use this to add extra slices of              vertices across the plane. This can be useful for some types of             vertex-based effects!|
|RETURNS: Mesh|A plane mesh, pre-sized to the given dimensions.|
```csharp
static Mesh GeneratePlane(Vec2 dimensions, Vec3 planeNormal, Vec3 planeTopDirection, int subdivisions)
```
Generates a plane with an arbitrary orientation that is
optionally subdivided, pre-sized to the given dimensions. UV
coordinates start at the top left indicated with
'planeTopDirection'.

NOTE: This generates a completely new Mesh asset on the GPU, and
is best done during 'initialization' of your app/scene. You may
also be interested in using the pre-generated `Mesh.Quad` asset
if it already meets your needs.

|  |  |
|--|--|
|Vec2 dimensions|How large is this plane on the XZ axis,              in meters?|
|Vec3 planeNormal|What is the normal of the surface this             plane is generated on?|
|Vec3 planeTopDirection|A normal defines the plane, but              this is technically a rectangle on the              plane. So which direction is up? It's important for UVs, but              doesn't need to be exact. This function takes the planeNormal as             law, and uses this vector to find the right and up vectors via             cross-products.|
|int subdivisions|Use this to add extra slices of              vertices across the plane. This can be useful for some types of             vertex-based effects!|
|RETURNS: Mesh|A plane mesh, pre-sized to the given dimensions.|


## Mesh.GenerateCube
```csharp
static Mesh GenerateCube(Vec3 dimensions, int subdivisions)
```
Generates a flat-shaded cube mesh, pre-sized to the
given dimensions. UV coordinates are projected flat on each face,
0,0 -> 1,1.

NOTE: This generates a completely new Mesh asset on the GPU, and
is best done during 'initialization' of your app/scene. You may
also be interested in using the pre-generated `Mesh.Cube` asset
if it already meets your needs.

|  |  |
|--|--|
|Vec3 dimensions|How large is this cube on each axis, in              meters?|
|int subdivisions|Use this to add extra slices of             vertices across the cube's              faces. This can be useful for some types of vertex-based effects             !|
|RETURNS: Mesh|A flat-shaded cube mesh, pre-sized to the given dimensions.|


## Mesh.GenerateRoundedCube
```csharp
static Mesh GenerateRoundedCube(Vec3 dimensions, float edgeRadius, int subdivisions)
```
Generates a cube mesh with rounded corners, pre-sized to
the given dimensions. UV coordinates are 0,0 -> 1,1 on each face,
meeting at the middle of the rounded corners.

NOTE: This generates a completely new Mesh asset on the GPU, and
is best done during 'initialization' of your app/scene.

|  |  |
|--|--|
|Vec3 dimensions|How large is this cube on each axis, in             meters?|
|float edgeRadius|Radius of the corner rounding, in             meters.|
|int subdivisions|How many subdivisions should be used             for creating the corners?              A larger value results in smoother corners, but can decrease             performance.|
|RETURNS: Mesh|A cube mesh with rounded corners, pre-sized to the given dimensions.|


## Mesh.GenerateSphere
```csharp
static Mesh GenerateSphere(float diameter, int subdivisions)
```
Generates a sphere mesh, pre-sized to the given
diameter, created by sphereifying a subdivided cube! UV
coordinates are taken from the initial unspherified cube.

NOTE: This generates a completely new Mesh asset on the GPU, and
is best done during 'initialization' of your app/scene. You may
also be interested in using the pre-generated `Mesh.Sphere` asset
if it already meets your needs.

|  |  |
|--|--|
|float diameter|The diameter of the sphere in meters, or              2*radius. This is the full length from one side to the other.|
|int subdivisions|How many times should the initial cube             be subdivided?|
|RETURNS: Mesh|A sphere mesh, pre-sized to the given diameter, created by sphereifying a subdivided cube! UV coordinates are taken from the initial unspherified cube.|


## Mesh.GenerateCylinder
```csharp
static Mesh GenerateCylinder(float diameter, float depth, Vec3 direction, int subdivisions)
```
Generates a cylinder mesh, pre-sized to the given
diameter and depth, UV coordinates are from a flattened top view
right now. Additional development is needed for making better UVs
for the edges.

NOTE: This generates a completely new Mesh asset on the GPU, and
is best done during 'initialization' of your app/scene.

|  |  |
|--|--|
|float diameter|Diameter of the circular part of the             cylinder in meters. Diameter is 2*radius.|
|float depth|How tall is this cylinder, in meters?|
|Vec3 direction|What direction do the circular surfaces              face? This is the surface normal for the top, it does not need to             be normalized.|
|int subdivisions|How many vertices compose the edges of             the cylinder? More is smoother, but less performant.|
|RETURNS: Mesh|Returns a cylinder mesh, pre-sized to the given diameter and depth, UV coordinates are from a flattened top view right now.|


## Mesh.Find
```csharp
static Mesh Find(string meshId)
```
Finds the Mesh with the matching id, and returns a
reference to it. If no Mesh it found, it returns null.

|  |  |
|--|--|
|string meshId|Id of the Mesh we're looking for.|
|RETURNS: Mesh|A Mesh with a matching id, or null if none is found.|


## Mesh.Sphere

```csharp
static Mesh Sphere{ get }
```

A sphere mesh with a diameter of 1. This is equivalent
to Mesh.GenerateSphere(1,4).

## Mesh.Cube

```csharp
static Mesh Cube{ get }
```

A cube with dimensions of (1,1,1), this is equivalent to
Mesh.GenerateCube(Vec3.One).

## Mesh.Quad

```csharp
static Mesh Quad{ get }
```

A default quad mesh, 2 triangles, 4 verts, from
(-0.5,-0.5,0) to (0.5,0.5,0) and facing forward on the Z axis
(0,0,-1). White vertex colors, and UVs from (1,1) at vertex
(-0.5,-0.5,0) to (0,0) at vertex (0.5,0.5,0).
# class Model

A Model is a collection of meshes, materials, and transforms
that make up a visual element! This is a great way to group together
complex objects that have multiple parts in them, and in fact, most
model formats are composed this way already!

This class contains a number of methods for creation. If you pass in
a .obj, .stl, , .ply (ASCII), .gltf, or .glb, StereoKit will load
that model from file, and assemble materials and transforms from the
file information. But you can also assemble a model from procedurally
generated meshes!

Because models include an offset transform for each mesh element,
this does have the overhead of an extra matrix multiplication in
order to execute a render command. So if you need speed, and only
have a single mesh with a precalculated transform matrix, it can be
faster to render a Mesh instead of a Model!

## Model.SubsetCount

```csharp
int SubsetCount{ get }
```

The number of mesh subsets attached to this model.

## Model.Nodes

```csharp
ModelNodeCollection Nodes{ get }
```

This is an enumerable collection of all the nodes in this
Model, ordered non-hierarchically by when they were added. You can
do Linq stuff with it, foreach it, or just treat it like a List or
array!

## Model.Visuals

```csharp
ModelVisualCollection Visuals{ get }
```

This is an enumerable collection of all the nodes with
Mesh/Material data in this Model, ordered non-hierarchically by
when they were added. You can do Linq stuff with it, foreach it, or
just treat it like a List or array!

## Model.Anims

```csharp
ModelAnimCollection Anims{ get }
```

An enumerable collection of animations attached to this
Model. You can do Linq stuff with it, foreach it, or just treat it
like a List or array!

## Model.Bounds

```csharp
Bounds Bounds{ get set }
```

This is a bounding box that encapsulates the Model and
all its subsets! It's used for collision, visibility testing, UI
layout, and probably other things. While it's normally calculated
from the mesh bounds, you can also override this to suit your
needs.

## Model.RootNode

```csharp
ModelNode RootNode{ get }
```

Returns the first root node in the Model's hierarchy.
There may be additional root nodes, and these will be Siblings
of this ModelNode. If there are no nodes present on the Model,
this will be null.

## Model.Model
```csharp
void Model(Mesh mesh, Material material)
```
Creates a single mesh subset Model using the indicated
Mesh and Material! An id will be automatically generated for this
asset.

|  |  |
|--|--|
|Mesh mesh|Any Mesh asset.|
|Material material|Any Material asset.|
```csharp
void Model()
```
Creates an empty Model object with an automatically
generated id. Use the AddSubset methods to fill this model out.
```csharp
void Model(string id, Mesh mesh, Material material)
```
Creates a single mesh subset Model using the indicated
Mesh and Material!

|  |  |
|--|--|
|string id|Uses this as the id, so you can Find it later.|
|Mesh mesh|Any Mesh asset.|
|Material material|Any Material asset.|


## Model.Copy
```csharp
Model Copy()
```
Creates a shallow copy of a Model asset! Meshes and
Materials referenced by this Model will be referenced, not
copied.

|  |  |
|--|--|
|RETURNS: Model|A new shallow copy of a Model.|


## Model.GetName
```csharp
string GetName(int subsetIndex)
```
Returns the name of the specific subset! This will be
the node name of your model asset. If no node name is available,
SteroKit will generate a name in the format of "subsetX", where
X would be the subset index. Note that names are not guaranteed
to be unique (users may assign the same name to multiple nodes).
Some nodes may also produce multiple subsets with the same name,
such as when a node contains a Mesh with multiple Materials, each
Mesh/Material combination will receive a subset with the same
name.

|  |  |
|--|--|
|int subsetIndex|Index of the model subset to get the              Material for, should be less than SubsetCount.|
|RETURNS: string|See summary for details.|


## Model.GetMaterial
```csharp
Material GetMaterial(int subsetIndex)
```
Gets a link to the Material asset used by the model
subset! Note that this is not necessarily a unique material, and
could be shared in a number of other places. Consider copying and
replacing it if you intend to modify it!

|  |  |
|--|--|
|int subsetIndex|Index of the model subset to get the              Material for, should be less than SubsetCount.|
|RETURNS: Material|A link to the Material asset used by the model subset at subsetIndex|


## Model.GetMesh
```csharp
Mesh GetMesh(int subsetIndex)
```
Gets a link to the Mesh asset used by the model subset!
Note that this is not necessarily a unique mesh, and could be
shared in a number of other places. Consider copying and
replacing it if you intend to modify it!

|  |  |
|--|--|
|int subsetIndex|Index of the model subset to get the             Mesh for, should be less than SubsetCount.|
|RETURNS: Mesh|A link to the Mesh asset used by the model subset at subsetIndex|


## Model.GetTransform
```csharp
Matrix GetTransform(int subsetIndex)
```
Gets the transform matrix used by the model subset!

|  |  |
|--|--|
|int subsetIndex|Index of the model subset to get the              transform for, should be less than SubsetCount.|
|RETURNS: Matrix|A transform matrix used by the model subset at subsetIndex|


## Model.SetMaterial
```csharp
void SetMaterial(int subsetIndex, Material material)
```
Changes the Material for the subset to a new one!

|  |  |
|--|--|
|int subsetIndex|Index of the model subset to replace,              should be less than SubsetCount.|
|Material material|The new Material, cannot be null.|


## Model.SetMesh
```csharp
void SetMesh(int subsetIndex, Mesh mesh)
```
Changes the mesh for the subset to a new one!

|  |  |
|--|--|
|int subsetIndex|Index of the model subset to replace,              should be less than SubsetCount.|
|Mesh mesh|The new Mesh, cannot be null.|


## Model.SetTransform
```csharp
void SetTransform(int subsetIndex, Matrix& transform)
```
Changes the transform for the subset to a new one! This
is in Model space, so it's relative to the origin of the model.

|  |  |
|--|--|
|int subsetIndex|Index of the transform to replace,             should be less than SubsetCount.|
|Matrix& transform|The new transform.|


## Model.AddSubset
```csharp
int AddSubset(Mesh mesh, Material material, Matrix& transform)
```
Adds a new subset to the Model, and recalculates the
bounds. A default subset name of "subsetX" will be used, where X
is the subset's index.

|  |  |
|--|--|
|Mesh mesh|The Mesh for the subset, may not be null.|
|Material material|The Material for the subset, may not be              null.|
|Matrix& transform|A transform Matrix representing the              Mesh's location relative to the origin of the Model.|
|RETURNS: int|The index of the subset that was just added.|
```csharp
int AddSubset(string name, Mesh mesh, Material material, Matrix& transform)
```
Adds a new subset to the Model, and recalculates the
bounds.

|  |  |
|--|--|
|string name|The text name of the subset. If this is null,             then a default name of "subsetX" will be used, where X is the              subset's index.|
|Mesh mesh|The Mesh for the subset, may not be null.|
|Material material|The Material for the subset, may not be              null.|
|Matrix& transform|A transform Matrix representing the              Mesh's location relative to the origin of the Model.|
|RETURNS: int|The index of the subset that was just added.|


## Model.RemoveSubset
```csharp
void RemoveSubset(int subsetIndex)
```
Removes and dereferences a subset from the model.

|  |  |
|--|--|
|int subsetIndex|Index of the subset to remove, should             be less than SubsetCount.|


## Model.PlayAnim
```csharp
void PlayAnim(string animationName, AnimMode mode)
```
Searches for an animation with the given name, and if it's
found, sets it up as the active animation and begins playing it
with the animation mode.

|  |  |
|--|--|
|string animationName|Case sensitive name of the animation.|
|AnimMode mode|The mode with which to play the animation.|
```csharp
void PlayAnim(Anim animation, AnimMode mode)
```
Sets the animation up as the active animation, and begins
playing it with the animation mode.

|  |  |
|--|--|
|Anim animation|The new active animation.|
|AnimMode mode|The mode with which to play the animation.|


## Model.StepAnim
```csharp
void StepAnim()
```
Calling Draw will automatically step the Model's
animation, but if you don't draw the Model, or need access to the
animated nodes before drawing, then you can step the animation
early manually via this method. Animation will only ever be stepped
once per frame, so it's okay to call this multiple times, or in
addition to Draw.


## Model.FindAnim
```csharp
Anim FindAnim(string name)
```
Searches the list of animations for the first one matching
the given name.

|  |  |
|--|--|
|string name|Case sensitive name of the animation.|
|RETURNS: Anim|A link to the animation, or null if none is found.|


## Model.ActiveAnim

```csharp
Anim ActiveAnim{ get }
```

This is a link to the currently active animation. If no
animation is active, this value will be null. To set the active
animation, use `PlayAnim`.

## Model.AnimTime

```csharp
float AnimTime{ get set }
```

This is the current time of the active animation in
seconds, from the start of the animation. If no animation is
active, this will be zero. This will always be a value between
zero and the active animation's `Duration`. For a percentage of
completion, see `AnimCompletion` instead.

## Model.AnimCompletion

```csharp
float AnimCompletion{ get set }
```

This is the percentage of completion of the active
animation. This will always be a value between 0-1. If no animation
is active, this will be zero.

## Model.AnimMode

```csharp
AnimMode AnimMode{ get }
```

The playback mode of the active animation.

## Model.Intersect
```csharp
bool Intersect(Ray modelSpaceRay, Ray& modelSpaceAt)
```
Checks the intersection point of this ray and a Model's
visual nodes. This will skip any node that is not flagged as Solid,
as well as any Mesh without collision data. Ray must be in model
space, intersection point will be in model space too. You can use
the inverse of the mesh's world transform matrix to bring the ray
into model space, see the example in the docs!

|  |  |
|--|--|
|Ray modelSpaceRay|Ray must be in model space, the             intersection point will be in model space too. You can use the             inverse of the mesh's world transform matrix to bring the ray             into model space, see the example in the docs!|
|Ray& modelSpaceAt|The intersection point and surface             direction of the ray and the mesh, if an intersection occurs.             This is in model space, and must be transformed back into world             space later. Direction is not guaranteed to be normalized,              especially if your own model->world transform contains scale/skew             in it.|
|RETURNS: bool|True if an intersection occurs, false otherwise!|


## Model.AddNode
```csharp
ModelNode AddNode(string name, Matrix modelTransform, Mesh mesh, Material material, bool solid)
```
This adds a root node to the `Model`'s node hierarchy! If
There is already an initial root node, this node will still be a
root node, but will be a `Sibling` of the `Model`'s `RootNode`. If
this is the first root node added, you'll be able to access it via
`RootNode`.

|  |  |
|--|--|
|string name|A text name to identify the node. If null is             provided, it will be auto named to "node"+index.|
|Matrix modelTransform|A Matrix describing this node's             transform in Model space.|
|Mesh mesh|The Mesh to attach to this Node's visual, if             this is null, then material must also be null.|
|Material material|The Material to attach to this Node's             visual, if this is null, then mesh must also be null.|
|bool solid|A flag that indicates the Mesh for this node             will be used in ray intersection tests. This flag is ignored if no             Mesh is attached.|
|RETURNS: ModelNode|This returns the newly added ModelNode, or if there's an issue with mesh and material being inconsistently null, then this result will also be null.|


## Model.FindNode
```csharp
ModelNode FindNode(string name)
```
Searches the entire list of Nodes, and will return the
first on that matches this name exactly. If no ModelNode is found,
then this will return null. Node Names are not guaranteed to be
unique.

|  |  |
|--|--|
|string name|Exact name to match against. ASCII only for now.|
|RETURNS: ModelNode|The first matching ModelNode, or null if none are found.|


## Model.RecalculateBounds
```csharp
void RecalculateBounds()
```
Examines the visuals as they currently are, and rebuilds
the bounds based on that! This is normally done automatically,
but if you modify a Mesh that this Model is using, the Model
can't see it, and you should call this manually.


## Model.Draw
```csharp
void Draw(Matrix transform, Color colorLinear, RenderLayer layer)
```
Adds this Model to the render queue for this frame! If
the Hierarchy has a transform on it, that transform is combined
with the Matrix provided here.

|  |  |
|--|--|
|Matrix transform|A Matrix that will transform the Model             from Model Space into the current Hierarchy Space.|
|Color colorLinear|A per-instance linear space color value             to pass into the shader! Normally this gets used like a material             tint. If you're  adventurous and don't need per-instance colors,             this is a great spot to pack in extra per-instance data for the             shader!|
|RenderLayer layer|All visuals are rendered using a layer              bit-flag. By default, all layers are rendered, but this can be              useful for filtering out objects for different rendering              purposes! For example: rendering a mesh over the user's head from             a 3rd person perspective, but filtering it out from the 1st             person perspective.|
```csharp
void Draw(Matrix transform)
```
Adds this Model to the render queue for this frame! If
the Hierarchy has a transform on it, that transform is combined
with the Matrix provided here.

|  |  |
|--|--|
|Matrix transform|A Matrix that will transform the Model             from Model Space into the current Hierarchy Space.|


## Model.Find
```csharp
static Model Find(string modelId)
```
Looks for a Model asset that's already loaded, matching
the given id!

|  |  |
|--|--|
|string modelId|Which Model are you looking for?|
|RETURNS: Model|A link to the Model matching 'modelId', null if none is found.|


## Model.FromFile
```csharp
static Model FromFile(string file, Shader shader)
```
Loads a list of mesh and material subsets from a .obj,
.stl, .ply (ASCII), .gltf, or .glb file.

|  |  |
|--|--|
|string file|Name of the file to load! This gets prefixed             with the StereoKit asset folder if no drive letter is specified             in the path.|
|Shader shader|The shader to use for the model's materials!             If null, this will             automatically determine the best shader available to use.|
|RETURNS: Model|A Model created from the file, or null if the file failed to load!|


## Model.FromMemory
```csharp
static Model FromMemory(string filename, Byte[]& data, Shader shader)
```
Loads a list of mesh and material subsets from a .obj,
.stl, .ply (ASCII), .gltf, or .glb file stored in memory. Note
that this function won't work well on files that reference other
files, such as .gltf files with references in them.

|  |  |
|--|--|
|string filename|StereoKit still uses the filename of the             data for format discovery, but not asset Id creation. If you              don't have a real filename for the data, just pass in an             extension with a leading '.' character here, like ".glb".|
|Byte[]& data|The binary data of a model file, this is NOT              a raw array of vertex and index data!|
|Shader shader|The shader to use for the model's materials!             If null, this will automatically determine the best shader              available to use.|
|RETURNS: Model|A Model created from the file, or null if the file failed to load!|


## Model.FromMesh
```csharp
static Model FromMesh(Mesh mesh, Material material)
```
Creates a single mesh subset Model using the indicated
Mesh and Material! An id will be automatically generated for this
asset.

|  |  |
|--|--|
|Mesh mesh|Any Mesh asset.|
|Material material|Any Material asset.|
|RETURNS: Model|A Model composed of a single mesh and Material.|
```csharp
static Model FromMesh(string id, Mesh mesh, Material material)
```
Creates a single mesh subset Model using the indicated
Mesh and Material!

|  |  |
|--|--|
|string id|Uses this as the id, so you can Find it later.|
|Mesh mesh|Any Mesh asset.|
|Material material|Any Material asset.|
|RETURNS: Model|A Model composed of a single mesh and Material.|

# class ModelNode

This class is a link to a node in a Model's internal
hierarchy tree. It's composed of node information, and links to
the directly adjacent tree nodes.

## ModelNode.Sibling

```csharp
ModelNode Sibling{ get }
```

The next ModelNode in the hierarchy, at the same level as
this one. To the "right" on a hierarchy tree. Null if there are no
more ModelNodes in the tree there.

## ModelNode.Parent

```csharp
ModelNode Parent{ get }
```

The ModelNode above this one ("up") in the hierarchy tree,
or null if this is a root node.

## ModelNode.Child

```csharp
ModelNode Child{ get }
```

The first child node "below" on the hierarchy tree, or
null if there are none. To see all children, get the Child and then
iterate through its Siblings.

## ModelNode.Name

```csharp
string Name{ get set }
```

This is the ASCII name that identifies this ModelNode. It
is generally provided by the Model's file, but in the event no name
(or null name) is provided, the name will default to "node"+index.
Names are not required to be unique.

## ModelNode.Solid

```csharp
bool Solid{ get set }
```

A flag that indicates the Mesh for this node will be used
in ray intersection tests. This flag is ignored if no Mesh is
attached.

## ModelNode.Visible

```csharp
bool Visible{ get set }
```

Is this node flagged as visible? By default, this is true
for all nodes with visual elements attached. These nodes will not
be drawn or skinned if you set this flag to false. If a ModelNode
has no visual elements attached to it, it will always return false,
and setting this value will have no effect.

## ModelNode.ModelTransform

```csharp
Matrix ModelTransform{ get set }
```

The transform of this node relative to the Model itself.
This incorporates transforms from all parent nodes. Setting this
transform will update the LocalTransform, as well as all Child
nodes below this one.

## ModelNode.LocalTransform

```csharp
Matrix LocalTransform{ get set }
```

The transform of this node relative to the Parent node.
Setting this transform will update the ModelTransform, as well as
all Child nodes below this one.

## ModelNode.Mesh

```csharp
Mesh Mesh{ get set }
```

The Mesh associated with this node. May be null, or may
also be re-used elsewhere.

## ModelNode.Material

```csharp
Material Material{ get set }
```

The Model associated with this node. May be null, or may
also be re-used elsewhere.

## ModelNode.MoveSibling
```csharp
bool MoveSibling()
```
Advances this ModelNode class to the next Sibling in the
hierarchy tree. If it cannot, then it remains the same.

|  |  |
|--|--|
|RETURNS: bool|True if it moved to the Sibling, false if there was no Sibling to move to.|


## ModelNode.MoveParent
```csharp
bool MoveParent()
```
Moves this ModelNode class to the Parent up the hierarchy
tree. If it cannot, then it remains the same.

|  |  |
|--|--|
|RETURNS: bool|True if it moved to the Parent, false if there was no Parent to move to.|


## ModelNode.MoveChild
```csharp
bool MoveChild()
```
Moves this ModelNode class to the first Child of this
node. If it cannot, then it remains the same.

|  |  |
|--|--|
|RETURNS: bool|True if it moved to the Child, false if there was no Child to move to.|


## ModelNode.AddChild
```csharp
ModelNode AddChild(string name, Matrix localTransform, Mesh mesh, Material material, bool solid)
```
Adds a Child node below this node, at the end of the child
chain!

|  |  |
|--|--|
|string name|A text name to identify the node. If null is             provided, it will be auto named to "node"+index.|
|Matrix localTransform|A Matrix describing this node's             transform in local space relative to the currently selected node.|
|Mesh mesh|The Mesh to attach to this Node's visual, if             this is null, then material must also be null.|
|Material material|The Material to attach to this Node's             visual, if this is null, then mesh must also be null.|
|bool solid|A flag that indicates the Mesh for this node             will be used in ray intersection tests. This flag is ignored if no             Mesh is attached.|
|RETURNS: ModelNode|This returns the newly added ModelNode, or if there's an issue with mesh and material being inconsistently null, then this result will also be null.|

# class ModelNodeCollection

An enumerable for Model's ModelNodes

## ModelNodeCollection.Count

```csharp
int Count{ get }
```

This is the total number of nodes in the Model.

## ModelNodeCollection.Add
```csharp
ModelNode Add(string name, Matrix modelTransform, Mesh mesh, Material material, bool solid)
```
This adds a root node to the `Model`'s node hierarchy! If
There is already an initial root node, this node will still be a
root node, but will be a `Sibling` of the `Model`'s `RootNode`. If
this is the first root node added, you'll be able to access it via
`RootNode`.

|  |  |
|--|--|
|string name|A text name to identify the node. If null is             provided, it will be auto named to "node"+index.|
|Matrix modelTransform|A Matrix describing this node's             transform in Model space.|
|Mesh mesh|The Mesh to attach to this Node's visual, if             this is null, then material must also be null.|
|Material material|The Material to attach to this Node's             visual, if this is null, then mesh must also be null.|
|bool solid|A flag that indicates the Mesh for this node             will be used in ray intersection tests. This flag is ignored if no             Mesh is attached.|
|RETURNS: ModelNode|This returns the newly added ModelNode, or if there's an issue with mesh and material being inconsistently null, then this result will also be null.|

# class ModelVisualCollection

An enumerable for Model's visual ModelNodes

## ModelVisualCollection.Count

```csharp
int Count{ get }
```

This is the total number of nodes with visual data
attached to them.
# class Anim

A link to a Model's animation! You can use this to get some
basic information about the animation, or store it for reference. This
maintains a link to the Model asset, and will keep it alive as long as
this object lives.

## Anim.Name

```csharp
string Name{ get }
```

The name of the animation as provided by the original
asset.

## Anim.Duration

```csharp
float Duration{ get }
```

The duration of the animation at normal playback speed, in
seconds.
# class ModelAnimCollection

An enumerable for Model's Anims

## ModelAnimCollection.Count

```csharp
int Count{ get }
```

This is the total number of animations attached to the
model.
# class Shader

A shader is a piece of code that runs on the GPU, and
determines how model data gets transformed into pixels on screen!
It's more likely that you'll work more directly with Materials, which
shaders are a subset of.

With this particular class, you can mostly just look at it. It doesn't
do a whole lot. Maybe you can swap out the shader code or something
sometimes!

## Shader.Name

```csharp
string Name{ get }
```

The name of the shader, provided in the shader file
itself. Not the filename or id.

## Shader.FromMemory
```csharp
static Shader FromMemory(Byte[]& data)
```
Creates a shader asset from a precompiled StereoKit
Shader file stored as bytes!

|  |  |
|--|--|
|Byte[]& data|A precompiled StereoKit Shader file as bytes.|
|RETURNS: Shader|A shader from the given data, or null if it failed to load/compile.|


## Shader.FromFile
```csharp
static Shader FromFile(string file)
```
Loads a shader from a precompiled StereoKit Shader
(.sks) file! HLSL files can be compiled using the skshaderc tool
included in the NuGet package. This should be taken care of by
MsBuild automatically, but you may need to ensure your HLSL file
is a <SKShader /> item type in the .csproj for this to
work. You can also compile with the command line app manually if
you're compiling/distributing a shader some other way!

|  |  |
|--|--|
|string file|Path to a precompiled StereoKit Shader file!             If no .sks extension is part of this path, StereoKit will              automatically add it and check that first.|
|RETURNS: Shader|A shader from the given file, or null if it failed to load/compile.|


## Shader.Find
```csharp
static Shader Find(string shaderId)
```
Looks for a Shader asset that's already loaded, matching
the given id! Unless the id has been set manually, the id will be
the same as the shader's name provided in the metadata.

|  |  |
|--|--|
|string shaderId|For shaders loaded from file, this'll be              the shader's metadata name!|
|RETURNS: Shader|Link to a shader asset!|


## Shader.Default

```csharp
static Shader Default{ get }
```

This is a fast, general purpose shader. It uses a
texture for 'diffuse', a 'color' property for tinting the
material, and a 'tex_scale' for scaling the UV coordinates. For
lighting, it just uses a lookup from the current cubemap.

## Shader.PBR

```csharp
static Shader PBR{ get }
```

A physically based shader.

## Shader.PBRClip

```csharp
static Shader PBRClip{ get }
```

Same as ShaderPBR, but with a discard clip for
transparency.

## Shader.UI

```csharp
static Shader UI{ get }
```

A shader for UI or interactable elements, this'll be the
same as the Shader, but with an additional finger 'shadow' and
distance circle effect that helps indicate finger distance from
the surface of the object.

## Shader.UIBox

```csharp
static Shader UIBox{ get }
```

A shader for indicating interaction volumes! It renders
a border around the edges of the UV coordinates that will 'grow'
on proximity to the user's finger. It will discard pixels outside
of that border, but will also show the finger shadow. This is
meant to be an opaque shader, so it works well for depth LSR.

This shader works best on cube-like meshes where each face has
UV coordinates from 0-1.

Shader Parameters:
```color                - color
border_size          - meters
border_size_grow     - meters
border_affect_radius - meters```

## Shader.Unlit

```csharp
static Shader Unlit{ get }
```

Sometimes lighting just gets in the way! This is an
extremely simple and fast shader that uses a 'diffuse' texture
and a 'color' tint property to draw a model without any lighting
at all!

## Shader.UnlitClip

```csharp
static Shader UnlitClip{ get }
```

Sometimes lighting just gets in the way! This is an
extremely simple and fast shader that uses a 'diffuse' texture
and a 'color' tint property to draw a model without any lighting
at all! This shader will also discard pixels with an alpha of
zero.
# class Solid

A Solid is an object that gets simulated with physics! Once
you create a solid, it will continuously be acted upon by forces like
gravity and other objects. Solid does -not- draw anything on its own,
but you can ask a Solid for its current pose, and draw an object at
that pose!

Once you create a Solid, you need to define its shape using geometric
primitives, this is the AddSphere, AddCube, AddCapsule functions. You
can add more than one to a single Solid to get a more complex shape!

If you want to directly move a Solid, note the difference between the
Move function and the Teleport function. Move will change the
velocity for a single frame to travel through space to get to its
destination, while teleport will simply appear at its destination
without touching anything between.

## Solid.Solid
```csharp
void Solid(Vec3 position, Quat rotation, SolidType type)
```
Creates a Solid physics object and adds it to the
physics system.

|  |  |
|--|--|
|Vec3 position|Initial location of the Solid.|
|Quat rotation|Initial rotation of the Solid.|
|SolidType type|What sort of physics properties does this              object exhibit?|


## Solid.Enabled

```csharp
bool Enabled{ set }
```

Is the Solid enabled in the physics simulation? Set this
to false if you want to prevent physics from influencing this
solid!

## Solid.AddSphere
```csharp
void AddSphere(float diameter, float kilograms, Nullable`1 offset)
```
This adds a sphere to this solid's physical shape! This
is in addition to any other shapes you've added to this solid.

|  |  |
|--|--|
|float diameter|Twice the radius of the sphere! The total             size, in meters!|
|float kilograms|How many kilograms does this shape weigh?|
|Nullable`1 offset|Offset to this shape from the center of the             solid.|


## Solid.AddBox
```csharp
void AddBox(Vec3 dimensions, float kilograms, Nullable`1 offset)
```
This adds a box to this solid's physical shape! This is
in addition to any other shapes you've added to this solid.

|  |  |
|--|--|
|Vec3 dimensions|The total width, height, and depth of             the box in meters!|
|float kilograms|How many kilograms does this shape weigh?|
|Nullable`1 offset|Offset of this shape from the center of the             solid.|


## Solid.AddCapsule
```csharp
void AddCapsule(float diameter, float height, float kilograms, Nullable`1 offset)
```
This adds a capsule, a cylinder with rounded ends, to
this solid's physical shape! This is in addition to any other
shapes you've added to this solid.

|  |  |
|--|--|
|float diameter|The diameter of the cylinder, as well as             the capsule ends, in meters.|
|float height|The total width, height, and depth of the             box in meters!|
|float kilograms|How many kilograms does this shape weigh?|
|Nullable`1 offset|Offset of this shape from the center of the              solid.|


## Solid.SetType
```csharp
void SetType(SolidType type)
```
Changes the behavior type of the solid after it's
created.

|  |  |
|--|--|
|SolidType type|The new solid type!|


## Solid.SetEnabled
```csharp
void SetEnabled(bool enabled)
```
Same as Enabled. Is the Solid enabled in the physics
simulation? Set this to false if you want to prevent physics from
influencing this solid!

|  |  |
|--|--|
|bool enabled|False to disable physics on this object,              true to enable it.|


## Solid.Move
```csharp
void Move(Vec3 position, Quat rotation)
```
This moves the Solid from its current location through
space to the new location provided, colliding with things along
the way. This is achieved by applying the velocity and angular
velocity necessary to get to the destination in a single frame
during the next physics step, then restoring the previous
velocity info afterwards! See also Teleport for movement without
collision.

|  |  |
|--|--|
|Vec3 position|The destination position!|
|Quat rotation|The destination rotation!|


## Solid.Teleport
```csharp
void Teleport(Vec3 position, Quat rotation)
```
Moves the Solid to the new pose, without colliding with
objects on the way there.

|  |  |
|--|--|
|Vec3 position|The destination position!|
|Quat rotation|The destination rotation!|


## Solid.SetVelocity
```csharp
void SetVelocity(Vec3 metersPerSecond)
```
Sets the velocity of this Solid.

|  |  |
|--|--|
|Vec3 metersPerSecond|How fast should it be going, along             what vector?|


## Solid.SetAngularVelocity
```csharp
void SetAngularVelocity(Vec3 radiansPerSecond)
```
I wish I knew what angular velocity actually meant,
perhaps you know? It's rotation velocity of some kind or another!

|  |  |
|--|--|
|Vec3 radiansPerSecond|I think it's in radians. Maybe?|


## Solid.GetPose
```csharp
Pose GetPose()
```
Retrieves the current pose of the Solid from the physics
simulation.

|  |  |
|--|--|
|RETURNS: Pose|The Solid's current pose.|
```csharp
void GetPose(Pose& pose)
```
Retrieves the current pose of the Solid from the physics
simulation.

|  |  |
|--|--|
|Pose& pose|Out param for the Solid's current pose.|

# class Sound

This class represents a sound effect! Excellent for blips
and bloops and little clips that you might play around your scene.
Not great for long streams of audio like you might see in a podcast.
Right now, this supports .wav, .mp3, and procedurally generated
noises!

On HoloLens 2, sounds are automatically processed on the HPU, freeing
up the CPU for more of your app's code. To simulate this same effect
on your development PC, you need to enable spatial sound on your
audio endpoint. To do this, right click the speaker icon in your
system tray, navigate to "Spatial sound", and choose "Windows Sonic
for Headphones." For more information, visit
https://docs.microsoft.com/en-us/windows/win32/coreaudio/spatial-sound

## Sound.Duration

```csharp
float Duration{ get }
```

This will return the total length of the sound in
seconds.

## Sound.TotalSamples

```csharp
int TotalSamples{ get }
```

This will return the total number of audio samples used
by the sound! StereoKit currently uses 48,000 samples per second
for all audio.

## Sound.UnreadSamples

```csharp
int UnreadSamples{ get }
```

This is the maximum number of samples in the sound that
are currently available for reading via ReadSamples! ReadSamples
will reduce this number by the amount of samples read.

This is only really valid for Stream sounds, all other sound
types will just return 0.

## Sound.CursorSamples

```csharp
int CursorSamples{ get }
```

This is the current position of the playback cursor,
measured in samples from the start of the audio data.

## Sound.Play
```csharp
SoundInst Play(Vec3 at, float volume)
```
Plays the sound at the 3D location specified, using the
volume parameter as an additional volume control option! Sound
volume falls off from 3D location, and can also indicate
direction and location through spatial audio cues. So make sure
the position is where you want people to think it's from!
Currently, if this sound is playing somewhere else, it'll be
canceled, and moved to this location.

|  |  |
|--|--|
|Vec3 at|World space location for the audio to play at.|
|float volume|Volume modifier for the effect! 1 means full             volume, and 0 means completely silent.|
|RETURNS: SoundInst|Returns a link to the Sound's play instance, which you can use to track and modify how the sound plays after the initial conditions are set.|


## Sound.WriteSamples
```csharp
void WriteSamples(Single[]& samples)
```
Only works if this Sound is a stream type! This writes
a number of audio samples to the sample buffer, and samples
should be between -1 and +1. Streams are stored as ring buffers
of a fixed size, so writing beyond the capacity of the ring
buffer will overwrite the oldest samples.

StereoKit uses 48,000 samples per second of audio.

|  |  |
|--|--|
|Single[]& samples|An array of audio samples, where each             sample is between -1 and +1.|
```csharp
void WriteSamples(Single[]& samples, int sampleCount)
```
Only works if this Sound is a stream type! This writes
a number of audio samples to the sample buffer, and samples
should be between -1 and +1. Streams are stored as ring buffers
of a fixed size, so writing beyond the capacity of the ring
buffer will overwrite the oldest samples.

StereoKit uses 48,000 samples per second of audio.

|  |  |
|--|--|
|Single[]& samples|An array of audio samples, where each             sample is between -1 and +1.|
|int sampleCount|You can use this to write only a subset             of the samples in the array, rather than the entire array!|


## Sound.ReadSamples
```csharp
int ReadSamples(Single[]& samples)
```
This will read samples from the sound stream, starting
from the first unread sample. Check UnreadSamples for how many
samples are available to read.

|  |  |
|--|--|
|Single[]& samples|A pre-allocated buffer to read the samples             into! This function will stop reading when this buffer is full,              or when it runs out of unread samples.|
|RETURNS: int|Returns the number of samples that were read from the stream's buffer and written to the provided sample buffer.|


## Sound.Find
```csharp
static Sound Find(string modelId)
```
Looks for a Sound asset that's already loaded, matching the given id!

|  |  |
|--|--|
|string modelId|Which Sound are you looking for?|
|RETURNS: Sound|A link to the sound matching 'soundId', null if none is found.|


## Sound.FromFile
```csharp
static Sound FromFile(string filename)
```
Loads a sound effect from file! Currently, StereoKit
supports .wav and .mp3 files. Audio is converted to mono.

|  |  |
|--|--|
|string filename|Name of the audio file! Supports .wav and             .mp3 files.|
|RETURNS: Sound|A sound object, or null if something went wrong.|


## Sound.CreateStream
```csharp
static Sound CreateStream(float streamBufferDuration)
```
Create a sound used for streaming audio in or out! This
is useful for things like reading from a microphone stream, or
playing audio from a source streaming over the network, or even
procedural sounds that are generated on the fly!

Use stream sounds with the WriteSamples and ReadSamples
functions.

|  |  |
|--|--|
|float streamBufferDuration|How much audio time should             this stream be able to hold without writing back over itself?|
|RETURNS: Sound|A stream sound that can be read and written to.|


## Sound.FromSamples
```csharp
static Sound FromSamples(Single[] samplesAt48000s)
```
This function will create a sound from an array of
samples. Values should range from -1 to +1, and there should be
48,000 values per second of audio.

|  |  |
|--|--|
|Single[] samplesAt48000s|Values should range from -1 to +1,              and there should be 48,000 per second of audio.|
|RETURNS: Sound|Returns a sound effect from the samples provided! Or null if something went wrong.|


## Sound.Generate
```csharp
static Sound Generate(AudioGenerator generator, float duration)
```
This function will generate a sound from a function you
provide! The function is called once for each sample in the
duration. As an example, it may be called 48,000 times for each
second of duration.

|  |  |
|--|--|
|AudioGenerator generator|This function takes a time value as an             argument, which will range from 0-duration, and should return a             value from -1 - +1 representing the audio wave at that point in             time.|
|float duration|In seconds, how long should the sound be?|
|RETURNS: Sound|Returns a generated sound effect! Or null if something went wrong.|


## Sound.Click

```csharp
static Sound Click{ get }
```

A default click sound that lasts for 300ms. It's a
procedurally generated sound based on a mouse press, with extra
low frequencies in it.

## Sound.Unclick

```csharp
static Sound Unclick{ get }
```

A default click sound that lasts for 300ms. It's a
procedurally generated sound based on a mouse release, with extra
low frequencies in it.
# struct SoundInst

This represents a play instance of a Sound! You can get one
when you call Sound.Play(). This allows you to do things like cancel
a piece of audio early, or change the volume and position of it as
it's playing.

## SoundInst.Position

```csharp
Vec3 Position{ get set }
```

The 3D position in world space this sound instance is
currently playing at. If this instance is no longer valid, the
position will be at zero.

## SoundInst.Volume

```csharp
float Volume{ get set }
```

The volume multiplier of this Sound instance! A number
between 0 and 1, where 0 is silent, and 1 is full volume.

## SoundInst.IsPlaying

```csharp
bool IsPlaying{ get }
```

Is this Sound instance currently playing? For streaming
assets, this will be true even if they don't have any new data
in them, and they're just idling at the end of their data.

## SoundInst.Stop
```csharp
void Stop()
```
This stops the sound early if it's still playing.

# class Sprite

A Sprite is an image that's set up for direct 2D rendering,
without using a mesh or model! This is technically a wrapper over a
texture, but it also includes atlasing functionality, which can be
pretty important to performance! This is used a lot in UI, for image
rendering.

Atlasing is not currently implemented, it'll swap to Single for now.
But here's how it works!

StereoKit will batch your sprites into an atlas if you ask it to!
This puts all the images on a single texture to significantly reduce
draw calls when many images are present. Any time you add a sprite to
an atlas, it'll be marked as dirty and rebuilt at the end of the
frame. So it can be a good idea to add all your images to the atlas
on initialize rather than during execution!

Since rendering is atlas based, you also have only one material per
atlas. So this is why you might wish to put a sprite in one atlas or
another, so you can apply different

## Sprite.Aspect

```csharp
float Aspect{ get }
```

The aspect ratio of the sprite! This is width/height.
You may also be interested in the NormalizedDimensions property,
which are normalized to the 0-1 range.

## Sprite.Width

```csharp
int Width{ get }
```

Width of the sprite, in pixels.

## Sprite.Height

```csharp
int Height{ get }
```

Height of the sprite, in pixels.

## Sprite.NormalizedDimensions

```csharp
Vec2 NormalizedDimensions{ get }
```

Width and height of the sprite, normalized so the
maximum value is 1.

## Sprite.Draw
```csharp
void Draw(Matrix& transform, Color32 color)
```
Draw the sprite on a quad with the provided transform!

|  |  |
|--|--|
|Matrix& transform|A Matrix describing a transform from              model space to world space.|
|Color32 color|Per-instance color data for this render item.|
```csharp
void Draw(Matrix& transform)
```
Draw the sprite on a quad with the provided transform!

|  |  |
|--|--|
|Matrix& transform|A Matrix describing a transform from              model space to world space.|
```csharp
void Draw(Matrix& transform, TextAlign anchorPosition)
```
Draws the sprite at the location specified by the
transform matrix. A sprite is always sized in model space as 1 x
Aspect meters on the x and y axes respectively, so scale
appropriately. The 'position' attribute describes what corner of
the sprite you're specifying the transform of.

|  |  |
|--|--|
|Matrix& transform|A Matrix describing a transform from              model space to world space. A sprite is always sized in model             space as 1 x Aspect meters on the x and y axes respectively, so             scale appropriately and remember that your anchor position may             affect the transform as well.|
|TextAlign anchorPosition|Describes what corner of the sprite             you're specifying the transform of. The 'Anchor' point or             'Origin' of the Sprite.|
```csharp
void Draw(Matrix& transform, TextAlign anchorPosition, Color32 linearColor)
```
Draws the sprite at the location specified by the
transform matrix. A sprite is always sized in model space as 1 x
Aspect meters on the x and y axes respectively, so scale
appropriately. The 'position' attribute describes what corner of
the sprite you're specifying the transform of.

|  |  |
|--|--|
|Matrix& transform|A Matrix describing a transform from              model space to world space. A sprite is always sized in model             space as 1 x Aspect meters on the x and y axes respectively, so             scale appropriately and remember that your anchor position may             affect the transform as well.|
|TextAlign anchorPosition|Describes what corner of the sprite             you're specifying the transform of. The 'Anchor' point or             'Origin' of the Sprite.|
|Color32 linearColor|Per-instance color data for this render             item. It is unmodified by StereoKit, and is generally interpreted             as linear.|


## Sprite.FromFile
```csharp
static Sprite FromFile(string file, SpriteType type, string atlasId)
```
Create a sprite from an image file! This loads a Texture
from file, and then uses that Texture as the source for the
Sprite.

|  |  |
|--|--|
|string file|The filename of the image, an absolute              filename, or a filename relative to the assets folder. Supports              jpg, png, tga, bmp, psd, gif, hdr, pic.|
|SpriteType type|Should this sprite be atlased, or an             individual image? Adding this as an atlased image is better for             performance, but will cause the atlas to be rebuilt! Images that             take up too much space on the atlas, or might be loaded or              unloaded during runtime may be better as Single rather than             Atlased!|
|string atlasId|The name of which atlas the sprite should             belong to, this is only relevant if the SpriteType is Atlased.|
|RETURNS: Sprite|A Sprite asset, or null if the image failed to load!|


## Sprite.FromTex
```csharp
static Sprite FromTex(Tex image, SpriteType type, string atlasId)
```
Create a sprite from a Texture object!

|  |  |
|--|--|
|Tex image|The texture to build a sprite from. Must be a             valid, 2D image!|
|SpriteType type|Should this sprite be atlased, or an             individual image? Adding this as an atlased image is better for             performance, but will cause the atlas to be rebuilt! Images that             take up too much space on the atlas, or might be loaded or             unloaded during runtime may be better as Single rather than             Atlased!|
|string atlasId|The name of which atlas the sprite should              belong to, this is only relevant if the SpriteType is Atlased.|
|RETURNS: Sprite|A Sprite asset, or null if the image failed when adding to the sprite system!|

# class Tex

This is the texture asset class! This encapsulates 2D images,
texture arrays, cubemaps, and rendertargets! It can load any image
format that stb_image can, (jpg, png, tga, bmp, psd, gif, hdr, pic)
plus more later on, and you can also create textures procedurally.

## Tex.Id

```csharp
string Id{ set }
```

Allows you to set the Id of the texture to a specific
Id.

## Tex.Width

```csharp
int Width{ get }
```

The width of the texture, in pixels. This will be a
blocking call if AssetState is less than LoadedMeta.

## Tex.Height

```csharp
int Height{ get }
```

The height of the texture, in pixels. This will be a
blocking call if AssetState is less than LoadedMeta.

## Tex.Format

```csharp
TexFormat Format{ get }
```

The StereoKit format this texture was initialized with.
This will be a blocking call if AssetState is less than LoadedMeta.

## Tex.AssetState

```csharp
AssetState AssetState{ get }
```

Textures are loaded asyncronously, so this tells you the
current state of this texture! This also can tell if an error
occured, and what type of error it may have been.

## Tex.FallbackOverride

```csharp
Tex FallbackOverride{ set }
```

This will override the default fallback texutre that gets
used before the Tex has finished loading. This is useful for
textures with a specific purpose where the normal fallback texture
would appear strange, such as a metal/rough map.

## Tex.AddressMode

```csharp
TexAddress AddressMode{ get set }
```

When looking at a UV texture coordinate on this texture,
how do we handle values larger than 1, or less than zero? Do we
Wrap to the other side? Clamp it between 0-1, or just keep
Mirroring back and forth? Wrap is the default.

## Tex.SampleMode

```csharp
TexSample SampleMode{ get set }
```

When sampling a texture that's stretched, or shrunk
beyond its screen size, how do we handle figuring out which color
to grab from the texture? Default is Linear.

## Tex.Anisoptropy

```csharp
int Anisoptropy{ get set }
```

When SampleMode is set to Anisotropic, this is the number
of samples the GPU takes to figure out the correct color. Default
is 4, and 16 is pretty high.

## Tex.CubemapLighting

```csharp
SphericalHarmonics CubemapLighting{ get }
```

ONLY valid for cubemap textures! This will calculate a
spherical harmonics representation of the cubemap for use with
StereoKit's lighting. First call may take a frame or two of time,
but subsequent calls will pull from a cached value.

## Tex.Tex
```csharp
void Tex(TexType textureType, TexFormat textureFormat)
```
Sets up an empty texture container! Fill it with data
using SetColors next! Creates a default unique asset Id.

|  |  |
|--|--|
|TexType textureType|What type of texture is it? Just a 2D              Image? A Cubemap? Should it have mip-maps?|
|TexFormat textureFormat|What information is the texture              composed of? 32 bit colors, 64 bit colors, etc.|


## Tex.SetColors
```csharp
void SetColors(int width, int height, IntPtr data)
```
Set the texture's pixels using a pointer to a chunk of
memory! This is great if you're pulling in some color data from
native code, and don't want to pay the cost of trying to marshal
that data around.

|  |  |
|--|--|
|int width|Width in pixels of the texture. Powers of two             are generally best!|
|int height|Height in pixels of the texture. Powers of             two are generally best!|
|IntPtr data|A pointer to a chunk of memory containing color             data! Should be  width*height*size_of_texture_format bytes large.             Color data should definitely match the format provided when              constructing the texture!|
```csharp
void SetColors(int width, int height, Color32[]& data)
```
Set the texture's pixels using a color array! This
function should only be called on textures with a format of
Rgba32 or Rgba32Linear. You can call this as many times as you'd
like, even with different widths and heights. Calling this
multiple times will mark it as dynamic on the graphics card.
Calling this function can also result in building mip-maps, which
has a non-zero cost: use TexType.ImageNomips when creating the
Tex to avoid this.

|  |  |
|--|--|
|int width|Width in pixels of the texture. Powers of two             are generally best!|
|int height|Height in pixels of the texture. Powers of             two are generally best!|
|Color32[]& data|An array of 32 bit colors, should be a length             of `width*height`.|
```csharp
void SetColors(int width, int height, Color[]& data)
```
Set the texture's pixels using a color array! This
function should only be called on textures with a format of Rgba128.
You can call this as many times as you'd like, even with different
widths and heights. Calling this multiple times will mark it as
dynamic on the graphics card. Calling this function can also
result in building mip-maps, which has a non-zero cost: use
TexType.ImageNomips when creating the Tex to avoid this.

|  |  |
|--|--|
|int width|Width in pixels of the texture. Powers of two             are generally best!|
|int height|Height in pixels of the texture. Powers of             two are generally best!|
|Color[]& data|An array of 128 bit colors, should be a length             of `width*height`.|
```csharp
void SetColors(int width, int height, Byte[]& data)
```
Set the texture's pixels using a scalar array! This
function should only be called on textures with a format of R8.
You can call this as many times as you'd like, even with different
widths and heights. Calling this multiple times will mark it as
dynamic on the graphics card. Calling this function can also
result in building mip-maps, which has a non-zero cost: use
TexType.ImageNomips when creating the Tex to avoid this.

|  |  |
|--|--|
|int width|Width in pixels of the texture. Powers of two             are generally best!|
|int height|Height in pixels of the texture. Powers of             two are generally best!|
|Byte[]& data|An array of 8 bit values, should be a length             of `width*height`.|
```csharp
void SetColors(int width, int height, UInt16[]& data)
```
Set the texture's pixels using a scalar array! This
function should only be called on textures with a format of R16.
You can call this as many times as you'd like, even with different
widths and heights. Calling this multiple times will mark it as
dynamic on the graphics card. Calling this function can also
result in building mip-maps, which has a non-zero cost: use
TexType.ImageNomips when creating the Tex to avoid this.

|  |  |
|--|--|
|int width|Width in pixels of the texture. Powers of two             are generally best!|
|int height|Height in pixels of the texture. Powers of             two are generally best!|
|UInt16[]& data|An array of 16 bit values, should be a length             of `width*height`.|
```csharp
void SetColors(int width, int height, Single[]& data)
```
Set the texture's pixels using a scalar array! This
function should only be called on textures with a format of R32.
You can call this as many times as you'd like, even with different
widths and heights. Calling this multiple times will mark it as
dynamic on the graphics card. Calling this function can also
result in building mip-maps, which has a non-zero cost: use
TexType.ImageNomips when creating the Tex to avoid this.

|  |  |
|--|--|
|int width|Width in pixels of the texture. Powers of two             are generally best!|
|int height|Height in pixels of the texture. Powers of             two are generally best!|
|Single[]& data|An array of 32 bit values, should be a length             of `width*height`.|


## Tex.SetSize
```csharp
void SetSize(int width, int height)
```
Set the texture's size without providing any color data.
In most cases, you should probably just call SetColors instead,
but this can be useful if you're adding color data some other
way, such as when blitting or rendering to it.

|  |  |
|--|--|
|int width|Width in pixels of the texture. Powers of two             are generally best!|
|int height|Height in pixels of the texture. Powers of             two are generally best!|


## Tex.AddZBuffer
```csharp
void AddZBuffer(TexFormat depthFormat)
```
Only applicable if this texture is a rendertarget!
This creates and attaches a zbuffer surface to the texture for
use when rendering to it.

|  |  |
|--|--|
|TexFormat depthFormat|The format of the depth texture, must             be a depth format type!|


## Tex.SetLoadingFallback
```csharp
static void SetLoadingFallback(Tex loadingTexture)
```
This is the texture that all Tex objects will fall back to
by default if they are still loading. Assigning a texture here that
isn't fully loaded will cause the app to block until it is loaded.

|  |  |
|--|--|
|Tex loadingTexture|Any _valid_ texture here is fine.             Preferably loaded already, but doesn't have to be.|


## Tex.SetErrorFallback
```csharp
static void SetErrorFallback(Tex errorTexture)
```
This is the texture that all Tex objects with errors will
fall back to. Assigning a texture here that isn't fully loaded will
cause the app to block until it is loaded.

|  |  |
|--|--|
|Tex errorTexture|Any _valid_ texture here is fine.             Preferably loaded already, but doesn't have to be.|


## Tex.Find
```csharp
static Tex Find(string id)
```
Finds a texture that matches the given Id! Check out the
DefaultIds static class for some built-in ids.

|  |  |
|--|--|
|string id|Id of the texture asset.|
|RETURNS: Tex|A Tex asset with the given id, or null if none is found.|


## Tex.FromCubemapEquirectangular
```csharp
static Tex FromCubemapEquirectangular(string equirectangularCubemap, bool sRGBData, int loadPriority)
```
Creates a cubemap texture from a single equirectangular
image! You know, the ones that look like an unwrapped globe with
the poles all stretched out. It uses some fancy shaders and
texture blitting to create 6 faces from the equirectangular
image.

|  |  |
|--|--|
|string equirectangularCubemap|Filename of the             equirectangular image.|
|bool sRGBData|Is this image color data in sRGB format,             or is it normal/metal/rough/data that's not for direct display?             sRGB colors get converted to linear color space on the graphics             card, so getting this right can have a big impact on visuals.|
|int loadPriority|The priority sort order for this asset             in the async loading system. Lower values mean loading sooner.|
|RETURNS: Tex|A Cubemap texture asset!|
```csharp
static Tex FromCubemapEquirectangular(string equirectangularCubemap, SphericalHarmonics& lightingInfo, bool sRGBData, int loadPriority)
```
Creates a cubemap texture from a single equirectangular
image! You know, the ones that look like an unwrapped globe with
the poles all stretched out. It uses some fancy shaders and
texture blitting to create 6 faces from the equirectangular image.

|  |  |
|--|--|
|string equirectangularCubemap|Filename of the             equirectangular image.|
|SphericalHarmonics& lightingInfo|An out value that represents the             lighting information scraped from the cubemap! This can then be             passed to `Renderer.SkyLight`.|
|bool sRGBData|Is this image color data in sRGB format,             or is it normal/metal/rough/data that's not for direct display?             sRGB colors get converted to linear color space on the graphics             card, so getting this right can have a big impact on visuals.|
|int loadPriority|The priority sort order for this asset             in the async loading system. Lower values mean loading sooner.|
|RETURNS: Tex|A Cubemap texture asset!|


## Tex.FromFile
```csharp
static Tex FromFile(string file, bool sRGBData, int loadPriority)
```
Loads an image file directly into a texture! Supported
formats are: jpg, png, tga, bmp, psd, gif, hdr, pic. Asset Id
will be the same as the filename.

|  |  |
|--|--|
|string file|An absolute filename, or a filename relative             to the assets folder. Supports jpg, png, tga, bmp, psd, gif, hdr,             pic|
|bool sRGBData|Is this image color data in sRGB format,             or is it normal/metal/rough/data that's not for direct display?             sRGB colors get converted to linear color space on the graphics             card, so getting this right can have a big impact on visuals.|
|int loadPriority|The priority sort order for this asset             in the async loading system. Lower values mean loading sooner.|
|RETURNS: Tex|A Tex asset from the given file, or null if it failed to load.|


## Tex.FromFiles
```csharp
static Tex FromFiles(String[] files, bool sRGBData, int priority)
```
Loads an array of image files directly into a single
array texture! Array textures are often useful for shader
effects, layering, material merging, weird stuff, and will
generally need a specific shader to support it. Supported formats
are: jpg, png, tga, bmp, psd, gif, hdr, pic. Asset Id will be the
hash of all the filenames merged consecutively.

|  |  |
|--|--|
|String[] files|Absolute filenames, or a filenames relative             to the assets folder. Supports jpg, png, tga, bmp, psd, gif, hdr,             pic|
|bool sRGBData|Is this image color data in sRGB format,             or is it normal/metal/rough/data that's not for direct display?             sRGB colors get converted to linear color space on the graphics             card, so getting this right can have a big impact on visuals.|
|int priority|The priority sort order for this asset in             the async loading system. Lower values mean loading sooner.|
|RETURNS: Tex|A Tex asset from the given files, or null if it failed to load.|


## Tex.FromMemory
```csharp
static Tex FromMemory(Byte[]& imageFileData, bool sRGBData, int priority)
```
Loads an image file stored in memory directly into a
texture! Supported formats are: jpg, png, tga, bmp, psd, gif,
hdr, pic. Asset Id will be the same as the filename.

|  |  |
|--|--|
|Byte[]& imageFileData|The binary data of an image file,             this is NOT a raw RGB color array!|
|bool sRGBData|Is this image color data in sRGB format,             or is it normal/metal/rough/data that's not for direct display?             sRGB colors get converted to linear color space on the graphics             card, so getting this right can have a big impact on visuals.|
|int priority|The priority sort order for this asset in             the async loading system. Lower values mean loading sooner.|
|RETURNS: Tex|A Tex asset from the given file, or null if it failed to load.|


## Tex.FromColors
```csharp
static Tex FromColors(Color32[]& colors, int width, int height, bool sRGBData)
```
Creates a texture and sets the texture's pixels using a
color array! This will be an image of type `TexType.Image`, and
a format of `TexFormat.Rgba32` or `TexFormat.Rgba32Linear`
depending on the value of the sRGBData parameter.

|  |  |
|--|--|
|Color32[]& colors|An array of 32 bit colors, should be a             length of `width*height`.|
|int width|Width in pixels of the texture. Powers of two             are generally best!|
|int height|Height in pixels of the texture. Powers of             two are generally best!|
|bool sRGBData|Is this image color data in sRGB format,             or is it normal/metal/rough/data that's not for direct display?             sRGB colors get converted to linear color space on the graphics             card, so getting this right can have a big impact on visuals.|
|RETURNS: Tex|A Tex asset with TexType.Image and TexFormat.Rgba32 from the given array of colors.|
```csharp
static Tex FromColors(Color[]& colors, int width, int height, bool sRGBData)
```
Creates a texture and sets the texture's pixels using a
color array! Color values are converted to 32 bit colors, so this
means a memory allocation and conversion. Prefer the Color32
overload for performance, or create an empty Texture and use
SetColors for more flexibility. This will be an image of type
`TexType.Image`, and a format of `TexFormat.Rgba32` or
`TexFormat.Rgba32Linear` depending on the value of the sRGBData
parameter.

|  |  |
|--|--|
|Color[]& colors|An array of 128 bit colors, should be a             length of `width*height`.|
|int width|Width in pixels of the texture. Powers of two             are generally best!|
|int height|Height in pixels of the texture. Powers of             two are generally best!|
|bool sRGBData|Is this image color data in sRGB format,             or is it normal/metal/rough/data that's not for direct display?             sRGB colors get converted to linear color space on the graphics             card, so getting this right can have a big impact on visuals.|
|RETURNS: Tex|A Tex asset with TexType.Image and TexFormat.Rgba32 from the given array of colors.|


## Tex.FromCubemapFile
```csharp
static Tex FromCubemapFile(String[] cubeFaceFiles_xxyyzz, bool sRGBData, int priority)
```
Creates a cubemap texture from 6 different image files!
If you have a single equirectangular image, use
Tex.FromEquirectangular  instead. Asset Id will be the first
filename.

|  |  |
|--|--|
|String[] cubeFaceFiles_xxyyzz|6 image filenames, in order of             +X, -X, +Y, -Y, +Z, -Z.|
|bool sRGBData|Is this image color data in sRGB format,             or is it normal/metal/rough/data that's not for direct display?             sRGB colors get converted to linear color space on the graphics             card, so getting this right can have a big impact on visuals.|
|int priority|The priority sort order for this asset in             the async loading system. Lower values mean loading sooner.|
|RETURNS: Tex|A Tex asset from the given files, or null if any failed to load.|
```csharp
static Tex FromCubemapFile(String[] cubeFaceFiles_xxyyzz, SphericalHarmonics& lightingInfo, bool sRGBData, int priority)
```
Creates a cubemap texture from 6 different image files!
If you have a single equirectangular image, use
Tex.FromEquirectangular instead. Asset Id will be the first
filename.

|  |  |
|--|--|
|String[] cubeFaceFiles_xxyyzz|6 image filenames, in order of             +X, -X, +Y, -Y, +Z, -Z.|
|SphericalHarmonics& lightingInfo|An out value that represents the             lighting information scraped from the cubemap! This can then be             passed to `Renderer.SkyLight`.|
|bool sRGBData|Is this image color data in sRGB format,             or is it normal/metal/rough/data that's not for direct display?             sRGB colors get converted to linear color space on the graphics             card, so getting this right can have a big impact on visuals.|
|int priority|The priority sort order for this asset in             the async loading system. Lower values mean loading sooner.|
|RETURNS: Tex|A Tex asset from the given files, or null if any failed to load.|


## Tex.GenColor
```csharp
static Tex GenColor(Color color, int width, int height, TexType type, TexFormat format)
```
This generates a solid color texture of the given
dimensions. Can be quite nice for creating placeholder textures!
Make sure to match linear/gamma colors with the correct format.

|  |  |
|--|--|
|Color color|The color to use for the texture. This is             interpreted slightly differently based on what TexFormat gets used.|
|int width|Width of the final texture, in pixels.|
|int height|Height of the final texture, in pixels.|
|TexType type|Not all types here are applicable, but             TexType.Image or TexType.ImageNomips are good options here.|
|TexFormat format|Not all formats are supported, but this does             support a decent range. The provided color is interpreted slightly             different depending on this format.|
|RETURNS: Tex|A solid color image of width x height pixels.|


## Tex.GenCubemap
```csharp
static Tex GenCubemap(Gradient gradient, Vec3 gradientDirection, int resolution)
```
Generates a cubemap texture from a gradient and a
direction! These are entirely suitable for skyboxes, which you
can set via Renderer.SkyTex.

|  |  |
|--|--|
|Gradient gradient|A color gradient the generator will sample             from! This looks at the 0-1 range of the gradient.|
|Vec3 gradientDirection|This vector points to where the             'top' of the color gradient will go. Conversely, the 'bottom' of             the gradient will be opposite, and it'll blend along that axis.|
|int resolution|The square size in pixels of each cubemap             face! This generally doesn't need to be large, unless you have a             really complicated gradient.|
|RETURNS: Tex|A procedurally generated cubemap texture!|
```csharp
static Tex GenCubemap(Gradient gradient, SphericalHarmonics& lightingInfo, Vec3 gradientDirection, int resolution)
```
Generates a cubemap texture from a gradient and a
direction! These are entirely suitable for skyboxes, which you
can set via `Renderer.SkyTex`.

|  |  |
|--|--|
|Gradient gradient|A color gradient the generator will sample             from! This looks at the 0-1 range of the gradient.|
|SphericalHarmonics& lightingInfo|An out value that represents the             lighting information scraped from the cubemap! This can then be             passed to `Renderer.SkyLight`.|
|Vec3 gradientDirection|This vector points to where the             'top' of the color gradient will go. Conversely, the 'bottom' of             the gradient will be opposite, and it'll blend along that axis.|
|int resolution|The square size in pixels of each cubemap             face! This generally doesn't need to be large, unless you have a             really complicated gradient.|
|RETURNS: Tex|A procedurally generated cubemap texture!|
```csharp
static Tex GenCubemap(SphericalHarmonics& lighting, int resolution, float lightSpotSizePct, float lightSpotIntensity)
```
Creates a cubemap from SphericalHarmonics lookups! These
are entirely suitable for skyboxes, which you can set via
`Renderer.SkyTex`.

|  |  |
|--|--|
|SphericalHarmonics& lighting|Lighting information stored in a             SphericalHarmonics.|
|int resolution|The square size in pixels of each cubemap             face! This generally doesn't need to be large, as             SphericalHarmonics typically contain pretty low frequency             information.|
|float lightSpotSizePct|The size of the glowing spot added             in the primary light direction. You can kinda think of the unit             as a percentage of the cubemap face's size, but it's technically             a Chebyshev distance from the light's point on a 2m cube.|
|float lightSpotIntensity|The glowing spot's color is the             primary light direction's color, but multiplied by this value.             Since this method generates a 128bpp texture, this is not clamped             between 0-1, so feel free to go nuts here! Remember that              reflections will often cut down some reflection intensity.|
|RETURNS: Tex|A procedurally generated cubemap texture!|


## Tex.White

```csharp
static Tex White{ get }
```

Default 2x2 white opaque texture, this is the texture
referred to as 'white' in the shader texture defaults.

## Tex.Black

```csharp
static Tex Black{ get }
```

Default 2x2 black opaque texture, this is the texture
referred to as 'black' in the shader texture defaults.

## Tex.Flat

```csharp
static Tex Flat{ get }
```

Default 2x2 flat normal texture, this is a normal that
faces out from the, face, and has a color value of (0.5,0.5,1).
This is the texture referred to as 'flat' in the shader texture
defaults.

## Tex.Gray

```csharp
static Tex Gray{ get }
```

Default 2x2 middle gray (0.5,0.5,0.5) opaque texture,
this is the texture referred to as 'gray' in the shader texture
defaults.

## Tex.Rough

```csharp
static Tex Rough{ get }
```

Default 2x2 roughness color (1,1,0,1) texture, this is the
texture referred to as 'rough' in the shader texture defaults.

## Tex.DevTex

```csharp
static Tex DevTex{ get }
```

This is a white checkered grid texture used to easily add
visual features to materials. By default, this is used for the
loading fallback texture for all Tex objects.

## Tex.Error

```csharp
static Tex Error{ get }
```

This is a red checkered grid texture used to indicate some
sort of error has occurred. By default, this is used for the error
fallback texture for all Tex objects.
# class HandMenuItem

This is a collection of display and behavior information for
a single item on the hand menu.

## HandMenuItem.name

```csharp
string name
```

Display name of the item.

## HandMenuItem.image

```csharp
Sprite image
```

Display image of the item, null is fine!

## HandMenuItem.action

```csharp
HandMenuAction action
```

Describes the menu related behavior of this menu item,
should it close the menu? Open another layer? Go back to the
previous menu?

## HandMenuItem.callback

```csharp
Action callback
```

The callback that should be performed when this menu
item is selected.

## HandMenuItem.layerName

```csharp
string layerName
```

If the action is set to Layer, then this is the layer
name used to find the next layer for the menu!

## HandMenuItem.HandMenuItem
```csharp
void HandMenuItem(string name, Sprite image, Action callback, string layerName)
```
A menu item that'll load another layer when selected.

|  |  |
|--|--|
|string name|Display name of the item.|
|Sprite image|Display image of the item, null is fine!|
|Action callback|The callback that should be performed when this menu             item is selected.|
|string layerName|This is the layer name used to find the next layer              for the menu! Get the spelling right, try using const strings!|
```csharp
void HandMenuItem(string name, Sprite image, Action callback, HandMenuAction action)
```
Makes a menu item!

|  |  |
|--|--|
|string name|Display name of the item.|
|Sprite image|Display image of the item, null is fine!|
|Action callback|The callback that should be performed when this menu             item is selected.|
|HandMenuAction action|Describes the menu related behavior of this menu item,             should it close the menu? Open another layer? Go back to the             previous menu?|

# enum HandMenuAction

This enum specifies how HandMenuItems should behave
when selected! This is in addition to the HandMenuItem's
callback function.

## HandMenuAction.Layer

```csharp
static HandMenuAction Layer
```

Move to another menu layer.

## HandMenuAction.Back

```csharp
static HandMenuAction Back
```

Go back to the previous layer.

## HandMenuAction.Close

```csharp
static HandMenuAction Close
```

Close the hand menu entirely! We're finished here.
# class HandMenuRadial

A menu that shows up in circle around the user's
hand! Selecting an item can perform an action, or even spawn
a sub-layer of menu items. This is an easy way to store
actions out of the way, yet remain easily accessible to the
user.

The user faces their palm towards their head, and then makes
a grip motion to spawn the menu. The user can then perform actions
by making fast, direction based motions that are easy to build
muscle memory for.

## HandMenuRadial.HandMenuRadial
```csharp
void HandMenuRadial(HandRadialLayer[] menuLayers)
```
Creates a hand menu from the provided array of menu
layers! HandMenuRadial is an IStepper, so proper usage is to
add it to the Stepper list via `StereoKitApp.AddStepper`.

|  |  |
|--|--|
|HandRadialLayer[] menuLayers|Starting layer is always the first one             in the list! Layer names in the menu items refer to layer names             in this list.|


## HandMenuRadial.Show
```csharp
void Show(Vec3 at)
```
Force the hand menu to show at a specific location.
This will close the hand menu if it was already open, and resets
it to the root menu layer. Also plays an opening sound.

|  |  |
|--|--|
|Vec3 at|A world space position for the hand menu.|


## HandMenuRadial.Close
```csharp
void Close()
```
Closes the menu if it's open! Plays a closing sound.


## HandMenuRadial.Step
```csharp
void Step()
```
Part of IStepper, you shouldn't be calling this yourself.


## HandMenuRadial.Enabled

```csharp
bool Enabled{ get }
```

HandMenuRadial is always Enabled.

## HandMenuRadial.Initialize
```csharp
bool Initialize()
```
Part of IStepper, you shouldn't be calling this yourself.

|  |  |
|--|--|
|RETURNS: bool|Always returns true.|


## HandMenuRadial.Shutdown
```csharp
void Shutdown()
```
Part of IStepper, you shouldn't be calling this yourself.

# class HandRadialLayer

This class represents a single layer in the HandRadialMenu.
Each item in the layer is displayed around the radial menu's circle.

## HandRadialLayer.layerName

```csharp
string layerName
```

Name of the layer, this is used for layer traversal, so
make sure you get the spelling right! Perhaps use const strings
for these.

## HandRadialLayer.items

```csharp
HandMenuItem[] items
```

A list of menu items to display in this menu layer.

## HandRadialLayer.startAngle

```csharp
float startAngle
```

An angle offset for the layer, if you want a specific
orientation for the menu's contents. Note this may not behave as
expected if you're setting this manually and using the backAngle
as well.

## HandRadialLayer.backAngle

```csharp
float backAngle
```

What's the angle pointing towards the back button on this
menu? If there is a back button. This is used to orient the back
button towards the direction the finger just came from.

## HandRadialLayer.HandRadialLayer
```csharp
void HandRadialLayer(string name, HandMenuItem[] items)
```
Creates a menu layer, this overload will calculate a backAngle
if there are any back actions present in the item list.

|  |  |
|--|--|
|string name|Name of the layer, this is used for layer traversal, so             make sure you get the spelling right! Perhaps use const strings             for these.|
|HandMenuItem[] items|A list of menu items to display in this menu layer.|
```csharp
void HandRadialLayer(string name, float startAngle, HandMenuItem[] items)
```
Creates a menu layer with an angle offset for the layer's rotation!

|  |  |
|--|--|
|string name|Name of the layer, this is used for layer traversal, so             make sure you get the spelling right! Perhaps use const strings             for these.|
|float startAngle|An angle offset for the layer, if you want a specific             orientation for the menu's contents. Note this may not behave as             expected if you're setting this manually and using the backAngle              as well.|
|HandMenuItem[] items|A list of menu items to display in this menu layer.|

# struct Bounds

Bounds is an axis aligned bounding box type that can be used
for storing the sizes of objects, calculating containment,
intersections, and more!

While the constructor uses a center+dimensions for creating a bounds,
don't forget the static From* methods that allow you to define a Bounds
from different types of data!

## Bounds.center

```csharp
Vec3 center
```

The exact center of the Bounds!

## Bounds.dimensions

```csharp
Vec3 dimensions
```

The total size of the box, from one end to the other. This
is the width, height, and depth of the Bounds.

## Bounds.Bounds
```csharp
void Bounds(Vec3 center, Vec3 totalDimensions)
```
Creates a bounding box object!

|  |  |
|--|--|
|Vec3 center|The exact center of the box.|
|Vec3 totalDimensions|The total size of the box, from one             end to the other. This is the width, height, and depth of the             Bounds.|
```csharp
void Bounds(Vec3 totalDimensions)
```
Creates a bounding box object centered around zero!

|  |  |
|--|--|
|Vec3 totalDimensions|The total size of the box, from one             end to the other. This is the width, height, and depth of the             Bounds.|


## Bounds.FromCorner
```csharp
static Bounds FromCorner(Vec3 bottomLeftBack, Vec3 dimensions)
```
Create a bounding box from a corner, plus box dimensions.

|  |  |
|--|--|
|Vec3 bottomLeftBack|The -X,-Y,-Z corner of the box.|
|Vec3 dimensions|The total dimensions of the box.|
|RETURNS: Bounds|A Bounds object that extends from bottomLeftBack to bottomLeftBack+dimensions.|


## Bounds.FromCorners
```csharp
static Bounds FromCorners(Vec3 bottomLeftBack, Vec3 topRightFront)
```
Create a bounding box between two corner points.

|  |  |
|--|--|
|Vec3 bottomLeftBack|The -X,-Y,-Z corner of the box.|
|Vec3 topRightFront|The +X,+Y,+Z corner of the box.|
|RETURNS: Bounds|A Bounds object that extends from bottomLeftBack to topRightFront.|


## Bounds.Intersect
```csharp
bool Intersect(Ray ray, Vec3& at)
```
Calculate the intersection between a Ray, and these
bounds. Returns false if no intersection occurred, and 'at' will
contain the nearest intersection point to the start of the ray if
an intersection is found!

|  |  |
|--|--|
|Ray ray|Any Ray in the same coordinate space as the             Bounds.|
|Vec3& at|If the return is true, this point will be the             closest intersection point to the origin of the Ray.|
|RETURNS: bool|True if an intersection occurred, false if not.|


## Bounds.Contains
```csharp
bool Contains(Vec3 pt)
```
Does the Bounds contain the given point? This includes
points that are on the surface of the Bounds.

|  |  |
|--|--|
|Vec3 pt|A point in the same coordinate space as the             Bounds.|
|RETURNS: bool|True if the point is on, or in the Bounds.|
```csharp
bool Contains(Vec3 linePt1, Vec3 linePt2)
```
Does the Bounds contain or intersects with the given line?

|  |  |
|--|--|
|Vec3 linePt1|Start of the line|
|Vec3 linePt2|End of the line|
|RETURNS: bool|True if the line is in, or intersects with the bounds.|
```csharp
bool Contains(Vec3 linePt1, Vec3 linePt2, float radius)
```
Does the bounds contain or intersect with the given
capsule?

|  |  |
|--|--|
|Vec3 linePt1|Start of the capsule.|
|Vec3 linePt2|End of the capsule|
|float radius|Radius of the capsule.|
|RETURNS: bool|True if the capsule is in, or intersects with the bounds.|


## Bounds.Scale
```csharp
void Scale(float scale)
```
Scale this bounds. It will scale the center as well as	the dimensions!
Modifies this bounds object.

|  |  |
|--|--|
|float scale|Scale to apply|
```csharp
void Scale(Vec3 scale)
```
Scale this bounds. It will scale the center as well as	the dimensions!
Modifies this bounds object.

|  |  |
|--|--|
|Vec3 scale|Scale to apply|


## Bounds.Scaled
```csharp
Bounds Scaled(float scale)
```
Scale the bounds.
It will scale the center as well as	the dimensions!
Returns a new Bounds.

|  |  |
|--|--|
|float scale|Scale|
|RETURNS: Bounds|A new scaled bounds|
```csharp
Bounds Scaled(Vec3 scale)
```
Scale the bounds.
It will scale the center as well as	the dimensions!
Returns a new Bounds.

|  |  |
|--|--|
|Vec3 scale|Scale|
|RETURNS: Bounds|A new scaled bounds|

# struct Matrix

A Matrix in StereoKit is a 4x4 grid of numbers that is used
to represent a transformation for any sort of position or vector!
This is an oversimplification of what a matrix actually is, but it's
accurate in this case.

Matrices are really useful for transforms because you can chain
together all sorts of transforms into a single Matrix! A Matrix
transform really shines when applied to many positions, as the more
expensive operations get cached within the matrix values.

Multiple matrix transforms can be combined by multiplying them. In
StereoKit, to create a matrix that first scales an object, followed by
rotating it, and finally translating it you would use this order:

`Matrix M = Matrix.S(...) * Matrix.R(...) * Matrix.T(...);`

This order is related to the fact that StereoKit uses row-major order
to store matrices. Note that in other 3D frameworks and certain 3D math
references you may find column-major matrices, which would need the
reverse order (i.e. T*R*S), so please keep this in mind when creating
transformations.

Matrices are prominently used within shaders for mesh transforms!

## Matrix.m

```csharp
Matrix4x4 m
```

The internal, wrapped System.Numerics type. This can be
nice to have around so you can pass its fields as 'ref', which you
can't do with properties. You won't often need this, as implicit
conversions to System.Numerics types are also provided.

## Matrix.Implicit Conversions
```csharp
static Matrix Implicit Conversions(Matrix4x4 m)
```
Allows implicit conversion from System.Numerics.Matrix4x4
to StereoKit.Matrix.

|  |  |
|--|--|
|Matrix4x4 m|Any System.Numerics Matrix4x4.|
|RETURNS: Matrix|A StereoKit compatible matrix.|
```csharp
static Matrix4x4 Implicit Conversions(Matrix m)
```
Allows implicit conversion from StereoKit.Matrix to
System.Numerics.Matrix4x4

|  |  |
|--|--|
|Matrix m|Any StereoKit.Matrix.|
|RETURNS: Matrix4x4|A System.Numerics compatible matrix.|


## Matrix.*
```csharp
static Matrix *(Matrix a, Matrix b)
```
Multiplies two matrices together! This is a great way to
combine transform operations. Note that StereoKit's matrices are
row-major, and multiplication order is important! To translate,
then scale, multiple in order of 'translate * scale'.

|  |  |
|--|--|
|Matrix a|First Matrix.|
|Matrix b|Second Matrix.|
|RETURNS: Matrix|Result of matrix multiplication.|
```csharp
static Vec3 *(Matrix a, Vec3 b)
```
Multiplies the vector by the Matrix! Since only a 1x4
vector can be multiplied against a 4x4 matrix, this uses '1' for
the 4th element, so the result will also include translation! To
exclude translation, use `Matrix.TransformNormal`.

|  |  |
|--|--|
|Matrix a|A transform matrix.|
|Vec3 b|Any Vector.|
|RETURNS: Vec3|The Vec3 transformed by the matrix, including translation.|
```csharp
static Ray *(Matrix a, Ray b)
```
Transforms a Ray by the Matrix! The position and direction
are both multiplied by the matrix, accounting properly for which
should include translation, and which should not.

|  |  |
|--|--|
|Matrix a|A transform matrix.|
|Ray b|A Ray to be transformed.|
|RETURNS: Ray|A Ray transformed by the Matrix.|
```csharp
static Pose *(Matrix a, Pose b)
```
Transforms a Pose by the Matrix! The position and
orientation are both transformed by the matrix, accounting properly
for the Pose's quaternion.

|  |  |
|--|--|
|Matrix a|A transform matrix.|
|Pose b|A Pose to be transformed.|
|RETURNS: Pose|A Ray transformed by the Matrix.|


## Matrix.Identity

```csharp
static Matrix Identity{ get }
```

An identity Matrix is the matrix equivalent of '1'!
Transforming anything by this will leave it at the exact same
place.

## Matrix.Translation

```csharp
Vec3 Translation{ get set }
```

A fast Property that will return or set the translation
component embedded in this transform matrix.

## Matrix.Scale

```csharp
Vec3 Scale{ get }
```

Returns the scale embedded in this transform matrix. Not
exactly cheap, requires 3 sqrt calls, but is cheaper than calling
Decompose.

## Matrix.Rotation

```csharp
Quat Rotation{ get }
```

A slow function that returns the rotation quaternion
embedded in this transform matrix. This is backed by Decompose,
so if you need any additional info, it's better to just call
Decompose instead.

## Matrix.Pose

```csharp
Pose Pose{ get }
```

Extracts translation and rotation information from the
transform matrix, and makes a Pose from it! Not exactly fast.
This is backed by Decompose, so if you need any additional info,
it's better to just call Decompose instead.

## Matrix.Inverse

```csharp
Matrix Inverse{ get }
```

Creates an inverse matrix! If the matrix takes a point
from a -> b, then its inverse takes the point from b -> a.

## Matrix.Invert
```csharp
void Invert()
```
Inverts this Matrix! If the matrix takes a point from a
-> b, then its inverse takes the point from b -> a.


## Matrix.Transform
```csharp
Vec3 Transform(Vec3 point)
```
Transforms a point through the Matrix! This is basically
just multiplying a vector (x,y,z,1) with the Matrix.

|  |  |
|--|--|
|Vec3 point|The point to transform.|
|RETURNS: Vec3|The point transformed by the Matrix.|
```csharp
Ray Transform(Ray ray)
```
Shorthand to transform a ray though the Matrix! This
properly transforms the position with the point transform method,
and the direction with the direction transform method. Does not
normalize, nor does it preserve a normalized direction if the
Matrix contains scale data.

|  |  |
|--|--|
|Ray ray|A ray you wish to transform from one space to             another.|
|RETURNS: Ray|The transformed ray!|
```csharp
Pose Transform(Pose pose)
```
Shorthand for transforming a Pose! This will transform
the position of the Pose with the matrix, extract a rotation Quat
from the matrix and apply that to the Pose's orientation. Note
that extracting a rotation Quat is an expensive operation, so if
you're doing it more than once, you should cache the rotation
Quat and do this transform manually.

|  |  |
|--|--|
|Pose pose|The original pose.|
|RETURNS: Pose|The transformed pose.|


## Matrix.TransformNormal
```csharp
Vec3 TransformNormal(Vec3 normal)
```
Transforms a point through the Matrix, but excluding
translation! This is great for transforming vectors that are
-directions- rather than points in space. Use this to transform
normals and directions. The same as multiplying (x,y,z,0) with
the Matrix.

|  |  |
|--|--|
|Vec3 normal|The direction to transform.|
|RETURNS: Vec3|The direction transformed by the Matrix.|


## Matrix.Decompose
```csharp
bool Decompose(Vec3& translation, Quat& rotation, Vec3& scale)
```
Returns this transformation matrix to its original
translation, rotation and scale components. Not exactly a cheap
function. If this is not a transform matrix, there's a chance
this call will fail, and return false.

|  |  |
|--|--|
|Vec3& translation|XYZ translation of the matrix.|
|Quat& rotation|The rotation quaternion, some lossiness             may be encountered when composing/decomposing.|
|Vec3& scale|XYZ scale components.|
|RETURNS: bool|If this is not a transform matrix, there's a chance this call will fail, and return false.|


## Matrix.T
```csharp
static Matrix T(Vec3 translation)
```
Translate. Creates a translation Matrix!

|  |  |
|--|--|
|Vec3 translation|Move an object by this amount.|
|RETURNS: Matrix|A Matrix containing a simple translation!|
```csharp
static Matrix T(float x, float y, float z)
```
Translate. Creates a translation Matrix!

|  |  |
|--|--|
|float x|Move an object on the x axis by this amount.|
|float y|Move an object on the y axis by this amount.|
|float z|Move an object on the z axis by this amount.|
|RETURNS: Matrix|A Matrix containing a simple translation!|


## Matrix.R
```csharp
static Matrix R(Quat rotation)
```
Create a rotation matrix from a Quaternion.

|  |  |
|--|--|
|Quat rotation|A Quaternion describing the rotation for              this transform.|
|RETURNS: Matrix|A Matrix that will rotate by the provided Quaternion orientation.|
```csharp
static Matrix R(float pitchXDeg, float yawYDeg, float rollZDeg)
```
Create a rotation matrix from pitch, yaw, and roll
information. Units are in degrees.

|  |  |
|--|--|
|float pitchXDeg|Pitch, or rotation around the X axis, in             degrees.|
|float yawYDeg|Yaw, or rotation around the Y axis, in              degrees.|
|float rollZDeg|Roll, or rotation around the Z axis, in             degrees.|
|RETURNS: Matrix|A Matrix that will rotate by the provided pitch, yaw and roll.|
```csharp
static Matrix R(Vec3 pitchYawRollDeg)
```
Create a rotation matrix from pitch, yaw, and roll
information. Units are in degrees.

|  |  |
|--|--|
|Vec3 pitchYawRollDeg|Pitch (x-axis), yaw (y-axis), and              roll (z-axis) stored as x, y and z respectively in this Vec3.             Units are in degrees.|
|RETURNS: Matrix|A Matrix that will rotate by the provided pitch, yaw and roll.|


## Matrix.S
```csharp
static Matrix S(Vec3 scale)
```
Creates a scaling Matrix, where scale can be different
on each axis (non-uniform).

|  |  |
|--|--|
|Vec3 scale|How much larger or smaller this transform             makes things. Vec3.One is a good default, as Vec3.Zero will             shrink it to nothing!|
|RETURNS: Matrix|A non-uniform scaling matrix.|
```csharp
static Matrix S(float scale)
```
Creates a scaling Matrix, where the scale is the same on
each axis (uniform).

|  |  |
|--|--|
|float scale|How much larger or smaller this transform             makes things. 1 is a good default, as 0 will shrink it to nothing!             This will expand to a scale vector of (size, size, size)|
|RETURNS: Matrix|A uniform scaling matrix.|


## Matrix.TS
```csharp
static Matrix TS(Vec3 translation, float scale)
```
Translate, Scale. Creates a transform Matrix using both
these components!

|  |  |
|--|--|
|Vec3 translation|Move an object by this amount.|
|float scale|How much larger or smaller this transform             makes things. 1 is a good default, as 0 will shrink it to nothing!             This will expand to a scale vector of (size, size, size)|
|RETURNS: Matrix|A Matrix that combines translation and scale information into a single Matrix!|
```csharp
static Matrix TS(Vec3 translation, Vec3 scale)
```
Translate, Scale. Creates a transform Matrix using both
these components!

|  |  |
|--|--|
|Vec3 translation|Move an object by this amount.|
|Vec3 scale|How much larger or smaller this transform              makes things. Vec3.One is a good default, as Vec3.Zero will              shrink it to nothing!|
|RETURNS: Matrix|A Matrix that combines translation and scale information into a single Matrix!|
```csharp
static Matrix TS(float x, float y, float z, float scale)
```
Translate, Scale. Creates a transform Matrix using both
these components!

|  |  |
|--|--|
|float x|Move an object on the x axis by this amount.|
|float y|Move an object on the y axis by this amount.|
|float z|Move an object on the z axis by this amount.|
|float scale|How much larger or smaller this transform              makes things. Vec3.One is a good default, as Vec3.Zero will              shrink it to nothing!|
|RETURNS: Matrix|A Matrix that combines translation and scale information into a single Matrix!|
```csharp
static Matrix TS(float x, float y, float z, Vec3 scale)
```
Translate, Scale. Creates a transform Matrix using both
these components!

|  |  |
|--|--|
|float x|Move an object on the x axis by this amount.|
|float y|Move an object on the y axis by this amount.|
|float z|Move an object on the z axis by this amount.|
|Vec3 scale|How much larger or smaller this transform              makes things. Vec3.One is a good default, as Vec3.Zero will              shrink it to nothing!|
|RETURNS: Matrix|A Matrix that combines translation and scale information into a single Matrix!|


## Matrix.TR
```csharp
static Matrix TR(Vec3 translation, Quat rotation)
```
Translate, Rotate. Creates a transform Matrix using
these components!

|  |  |
|--|--|
|Vec3 translation|Move an object by this amount.|
|Quat rotation|A Quaternion describing the rotation for              this transform.|
|RETURNS: Matrix|A Matrix that combines translation and rotation information into a single Matrix!|
```csharp
static Matrix TR(float x, float y, float z, Quat rotation)
```
Translate, Rotate. Creates a transform Matrix using
these components!

|  |  |
|--|--|
|float x|Move an object on the x axis by this amount.|
|float y|Move an object on the y axis by this amount.|
|float z|Move an object on the z axis by this amount.|
|Quat rotation|A Quaternion describing the rotation for              this transform.|
|RETURNS: Matrix|A Matrix that combines translation and rotation information into a single Matrix!|
```csharp
static Matrix TR(Vec3 translation, Vec3 pitchYawRollDeg)
```
Translate, Rotate. Creates a transform Matrix using
these components!

|  |  |
|--|--|
|Vec3 translation|Move an object by this amount.|
|Vec3 pitchYawRollDeg|Pitch (x-axis), yaw (y-axis), and              roll (z-axis) stored as x, y and z respectively in this Vec3.             Units are in degrees.|
|RETURNS: Matrix|A Matrix that combines translation and rotation information into a single Matrix!|
```csharp
static Matrix TR(float x, float y, float z, Vec3 pitchYawRollDeg)
```
Translate, Rotate. Creates a transform Matrix using
these components!

|  |  |
|--|--|
|float x|Move an object on the x axis by this amount.|
|float y|Move an object on the y axis by this amount.|
|float z|Move an object on the z axis by this amount.|
|Vec3 pitchYawRollDeg|Pitch (x-axis), yaw (y-axis), and              roll (z-axis) stored as x, y and z respectively in this Vec3.             Units are in degrees.|
|RETURNS: Matrix|A Matrix that combines translation and rotation information into a single Matrix!|


## Matrix.TRS
```csharp
static Matrix TRS(Vec3 translation, Quat rotation, float scale)
```
Translate, Rotate, Scale. Creates a transform Matrix
using all these components!

|  |  |
|--|--|
|Vec3 translation|Move an object by this amount.|
|Quat rotation|A Quaternion describing the rotation for              this transform.|
|float scale|How much larger or smaller this transform              makes things. 1 is a good default, as 0 will shrink it to nothing!              This will expand to a scale vector of (size, size, size)|
|RETURNS: Matrix|A Matrix that combines translation, rotation, and scale information into a single Matrix!|
```csharp
static Matrix TRS(Vec3 translation, Quat rotation, Vec3 scale)
```
Translate, Rotate, Scale. Creates a transform Matrix
using all these components!

|  |  |
|--|--|
|Vec3 translation|Move an object by this amount.|
|Quat rotation|A Quaternion describing the rotation for              this transform.|
|Vec3 scale|How much larger or smaller this transform              makes things. Vec3.One is a good default, as Vec3.Zero will              shrink it to nothing!|
|RETURNS: Matrix|A Matrix that combines translation, rotation, and scale information into a single Matrix!|
```csharp
static Matrix TRS(Vec3 translation, Vec3 pitchYawRollDeg, float scale)
```
Translate, Rotate, Scale. Creates a transform Matrix
using all these components!

|  |  |
|--|--|
|Vec3 translation|Move an object by this amount.|
|Vec3 pitchYawRollDeg|Pitch (x-axis), yaw (y-axis), and              roll (z-axis) stored as x, y and z respectively in this Vec3.             Units are in degrees.|
|float scale|How much larger or smaller this transform              makes things. Vec3.One is a good default, as Vec3.Zero will              shrink it to nothing!|
|RETURNS: Matrix|A Matrix that combines translation, rotation, and scale information into a single Matrix!|
```csharp
static Matrix TRS(Vec3 translation, Vec3 pitchYawRollDeg, Vec3 scale)
```
Translate, Rotate, Scale. Creates a transform Matrix
using all these components!

|  |  |
|--|--|
|Vec3 translation|Move an object by this amount.|
|Vec3 pitchYawRollDeg|Pitch (x-axis), yaw (y-axis), and              roll (z-axis) stored as x, y and z respectively in this Vec3.             Units are in degrees.|
|Vec3 scale|How much larger or smaller this transform              makes things. Vec3.One is a good default, as Vec3.Zero will              shrink it to nothing!|
|RETURNS: Matrix|A Matrix that combines translation, rotation, and scale information into a single Matrix!|


## Matrix.Perspective
```csharp
static Matrix Perspective(float fovDegrees, float aspectRatio, float nearClip, float farClip)
```
This creates a matrix used for projecting 3D geometry
onto a 2D surface for rasterization. Perspective projection
matrices will cause parallel lines to converge at the horizon.
This is great for normal looking content.

|  |  |
|--|--|
|float fovDegrees|This is the vertical field of view of             the perspective matrix, units are in degrees.|
|float aspectRatio|The projection surface's width/height.|
|float nearClip|Anything closer than this distance (in             meters) will be discarded. Must not be zero, and if you make this             too small, you may experience glitching in your depth buffer.|
|float farClip|Anything further than this distance (in             meters) will be discarded. For low resolution depth buffers, this             should not be too far away, or you'll see bad z-fighting              artifacts.|
|RETURNS: Matrix|The final perspective matrix.|


## Matrix.Orthographic
```csharp
static Matrix Orthographic(float width, float height, float nearClip, float farClip)
```
This creates a matrix used for projecting 3D geometry
onto a 2D surface for rasterization. Orthographic projection
matrices will preserve parallel lines. This is great for 2D
scenes or content.

|  |  |
|--|--|
|float width|The width, in meters, of the area that will              be projected.|
|float height|The height, in meters, of the area that will             be projected.|
|float nearClip|Anything closer than this distance (in             meters) will be discarded. Must not be zero, and if you make this             too small, you may experience glitching in your depth buffer.|
|float farClip|Anything further than this distance (in             meters) will be discarded. For low resolution depth buffers, this             should not be too far away, or you'll see bad z-fighting              artifacts.|
|RETURNS: Matrix|The final orthographic matrix.|

# struct Plane

Planes are really useful for collisions, intersections, and
visibility testing!

This plane is stored using the ax + by + cz + d = 0 formula, where
the normal is a,b,c, and the d is, well, d.

## Plane.p

```csharp
Plane p
```

The internal, wrapped System.Numerics type. This can be
nice to have around so you can pass its fields as 'ref', which
you can't do with properties. You won't often need this, as
implicit conversions to System.Numerics types are also
provided.

## Plane.normal

```csharp
Vec3 normal{ get set }
```

The direction the plane is facing.

## Plane.d

```csharp
float d{ get set }
```

The distance/travel along the plane's normal from the
origin to the surface of the plane.

## Plane.Plane
```csharp
void Plane(Vec3 normal, float d)
```
Creates a Plane directly from the ax + by + cz + d = 0
formula!

|  |  |
|--|--|
|Vec3 normal|Direction the plane is facing.|
|float d|Distance along the normal from the origin to the surface of the plane.|
```csharp
void Plane(Vec3 pointOnPlane, Vec3 planeNormal)
```
Creates a plane from a normal, and any point on the plane!

|  |  |
|--|--|
|Vec3 pointOnPlane|Any point directly on the surface of the plane.|
|Vec3 planeNormal|Direction the plane is facing.|
```csharp
void Plane(Vec3 pointOnPlane1, Vec3 pointOnPlane2, Vec3 pointOnPlane3)
```
Creates a plane from 3 points that are directly on that plane.

|  |  |
|--|--|
|Vec3 pointOnPlane1|First point on the plane.|
|Vec3 pointOnPlane2|Second point on the plane.|
|Vec3 pointOnPlane3|Third point on the plane.|


## Plane.Intersect
```csharp
bool Intersect(Ray ray, Vec3& at)
```
Checks the intersection of a ray with this plane!

|  |  |
|--|--|
|Ray ray|Ray we're checking with.|
|Vec3& at|An out parameter that will hold the intersection             point. If there's no intersection, this will be (0,0,0).|
|RETURNS: bool|True if there's an intersection, false if not. Refer to the 'at' parameter for intersection information!|
```csharp
bool Intersect(Vec3 lineStart, Vec3 lineEnd, Vec3& at)
```
Checks the intersection of a line with this plane!

|  |  |
|--|--|
|Vec3 lineStart|Start of the line.|
|Vec3 lineEnd|End of the line.|
|Vec3& at|An out parameter that will hold the intersection             point. If there's no intersection, this will be (0,0,0).|
|RETURNS: bool|True if there's an intersection, false if not. Refer to the 'at' parameter for intersection information!|


## Plane.Closest
```csharp
Vec3 Closest(Vec3 to)
```
Finds the closest point on this plane to the given
point!

|  |  |
|--|--|
|Vec3 to|The point you have that's not necessarily on the             plane.|
|RETURNS: Vec3|The point on the plane that's closest to the 'to' parameter.|


## Plane.FromPoint
```csharp
static Plane FromPoint(Vec3 pointOnPlane, Vec3 planeNormal)
```
Creates a plane from a normal, and any point on the
plane!

|  |  |
|--|--|
|Vec3 pointOnPlane|Any point directly on the surface of              the plane.|
|Vec3 planeNormal|Direction the plane is facing.|
|RETURNS: Plane|A plane that contains pointOnPlane, and faces planeNormal.|


## Plane.FromPoints
```csharp
static Plane FromPoints(Vec3 pointOnPlane1, Vec3 pointOnPlane2, Vec3 pointOnPlane3)
```
Creates a plane from 3 points that are directly on that
plane.

|  |  |
|--|--|
|Vec3 pointOnPlane1|First point on the plane.|
|Vec3 pointOnPlane2|Second point on the plane.|
|Vec3 pointOnPlane3|Third point on the plane.|
|RETURNS: Plane|A plane that contains all three points.|

# struct Pose

Pose represents a location and orientation in space,
excluding scale! CAUTION: the default value of a Pose includes a
completely zero Quat, which can cause problems. Use `Pose.Identity`
instead of `new Pose()` for creating a default pose.

## Pose.position

```csharp
Vec3 position
```

Location of the pose.

## Pose.orientation

```csharp
Quat orientation
```

Orientation of the pose, stored as a rotation from Vec3.Forward.

## Pose.Forward

```csharp
Vec3 Forward{ get }
```

Calculates the forward direction from this pose. This is done by
multiplying the orientation with Vec3.Forward. Remember that Forward points
down the -Z axis!

## Pose.Right

```csharp
Vec3 Right{ get }
```

Calculates the right (+X) direction from this pose. This is done by
multiplying the orientation with Vec3.Right.

## Pose.Up

```csharp
Vec3 Up{ get }
```

Calculates the up (+Y) direction from this pose. This is done by
multiplying the orientation with Vec3.Up.

## Pose.Ray

```csharp
Ray Ray{ get }
```

This creates a ray starting at the Pose's position, and
pointing in the 'Forward' direction. The Ray direction is a unit
vector/normalized.

## Pose.Identity

```csharp
static Pose Identity{ get }
```

A default, empty pose. Positioned at zero, and using
Quat.Identity for orientation.

## Pose.Pose
```csharp
void Pose(Vec3 position, Quat orientation)
```
Basic initialization constructor! Just copies in the provided values directly.

|  |  |
|--|--|
|Vec3 position|Location of the pose.|
|Quat orientation|Orientation of the pose, stored as a rotation from Vec3.Forward.|
```csharp
void Pose(float x, float y, float z, Quat orientation)
```
Basic initialization constructor! Just copies in the provided values directly.

|  |  |
|--|--|
|float x|X location of the pose.|
|float y|Y location of the pose.|
|float z|Z location of the pose.|
|Quat orientation|Orientation of the pose, stored as a rotation from Vec3.Forward.|


## Pose.ToMatrix
```csharp
Matrix ToMatrix(Vec3 scale)
```
Converts this pose into a transform matrix, incorporating a provided scale value.

|  |  |
|--|--|
|Vec3 scale|A scale vector! Vec3.One would be an identity scale.|
|RETURNS: Matrix|A Matrix that transforms to the given pose.|
```csharp
Matrix ToMatrix(float scale)
```
Converts this pose into a transform matrix, incorporating a provided scale value.

|  |  |
|--|--|
|float scale|A uniform scale factor! 1 would be an identity scale.|
|RETURNS: Matrix|A Matrix that transforms to the given pose.|
```csharp
Matrix ToMatrix()
```
Converts this pose into a transform matrix.

|  |  |
|--|--|
|RETURNS: Matrix|A Matrix that transforms to the given pose.|


## Pose.Lerp
```csharp
static Pose Lerp(Pose a, Pose b, float percent)
```
Interpolates between two poses! t is unclamped, so values outside of (0,1) will
extrapolate their position.

|  |  |
|--|--|
|Pose a|Starting pose, or percent == 0|
|Pose b|Ending pose, or percent == 1|
|float percent|A value usually 0->1 that tells the blend between a and b.|
|RETURNS: Pose|A new pose, blended between a and b based on percent!|

# struct Quat

Quaternions are efficient and robust mathematical objects for
representing rotations! Understanding the details of how a quaternion
works is not generally necessary for using them effectively, so don't
worry too much if they seem weird to you. They're weird to me too.

If you're interested in learning the details though, 3Blue1Brown and
Ben Eater have an [excellent interactive lesson](https://eater.net/quaternions)
about them!

## Quat.q

```csharp
Quaternion q
```

The internal, wrapped System.Numerics type. This can be
nice to have around so you can pass its fields as 'ref', which you
can't do with properties. You won't often need this, as implicit
conversions to System.Numerics types are also provided.

## Quat.x

```csharp
float x{ get set }
```

X component. Sometimes known as I.

## Quat.y

```csharp
float y{ get set }
```

Y component. Sometimes known as J.

## Quat.z

```csharp
float z{ get set }
```

Z component. Sometimes known as K.

## Quat.w

```csharp
float w{ get set }
```

W component.

## Quat.Vec4

```csharp
Vec4 Vec4{ get }
```

Sometimes you want to do weird stuff with your
Quaternions. I won't judge. This just turns the Quat into a Vec4,
makes some types of math easier!

## Quat.Quat
```csharp
void Quat(float x, float y, float z, float w)
```
You may want to use static creation methods, like
Quat.LookAt, or Quat.Identity instead of this one! Unless you
know what you're doing.

|  |  |
|--|--|
|float x|X component of the Quat.|
|float y|Y component of the Quat.|
|float z|Z component of the Quat.|
|float w|W component of the Quat.|


## Quat.Implicit Conversions
```csharp
static Quat Implicit Conversions(Quaternion q)
```
Allows implicit conversion from System.Numerics.Quaternion
to StereoKit.Quat.

|  |  |
|--|--|
|Quaternion q|Any System.Numerics Quaternion.|
|RETURNS: Quat|A StereoKit compatible quaternion.|
```csharp
static Quaternion Implicit Conversions(Quat q)
```
Allows implicit conversion from StereoKit.Quat to
System.Numerics.Quaternion

|  |  |
|--|--|
|Quat q|Any StereoKit.Quat.|
|RETURNS: Quaternion|A System.Numerics compatible quaternion.|


## Quat.Identity

```csharp
static Quat Identity
```

This is the 'multiply by one!' of the quaternion
rotation world. It's basically a default, no rotation quaternion.

## Quat.Normalized

```csharp
Quat Normalized{ get }
```

A normalized quaternion has the same orientation, and a
length of 1.

## Quat.Inverse

```csharp
Quat Inverse{ get }
```

The reverse rotation! If this quat goes from A to B, the
inverse will go from B to A.

## Quat.Normalize
```csharp
void Normalize()
```
A normalized quaternion has the same orientation, and a
length of 1.


## Quat.Invert
```csharp
void Invert()
```
Makes this Quat the reverse rotation! If this quat goes
from A to B, the inverse will go from B to A.


## Quat.LookAt
```csharp
static Quat LookAt(Vec3 lookFromPoint, Vec3 lookAtPoint, Vec3 upDirection)
```
Creates a rotation that describes looking from a point,
to another point! This is a great function for camera style
rotation, or other facing behavior when you know where an object
is, and where you want it to look at. This rotation works best
when applied to objects that face Vec3.Forward in their
resting/model space pose.

|  |  |
|--|--|
|Vec3 lookFromPoint|Position of where the 'object' is.|
|Vec3 lookAtPoint|Position of where the 'object' should             be looking towards!|
|Vec3 upDirection|Look From/At positions describe X and Y             axis rotation well, but leave Z Axis/Roll undefined. Providing an             upDirection vector helps to indicate roll around the From/At             line. A common up direction would be (0,1,0), to prevent roll.|
|RETURNS: Quat|A rotation that describes looking from a point, towards another point.|
```csharp
static Quat LookAt(Vec3 lookFromPoint, Vec3 lookAtPoint)
```
Creates a rotation that describes looking from a point,
to another point! This is a great function for camera style
rotation, or other facing behavior when you know where an object
is, and where you want it to look at. This rotation works best
when applied to objects that face Vec3.Forward in their
resting/model space pose.

This overload automatically defines 'upDirection' as (0,1,0).
This indicates the rotation should contain no roll around the Z
axis, and is the most common way of using this type of rotation.

|  |  |
|--|--|
|Vec3 lookFromPoint|Position of where the 'object' is.|
|Vec3 lookAtPoint|Position of where the 'object' should             be looking towards!|
|RETURNS: Quat|A rotation that describes looking from a point, towards another point.|


## Quat.LookDir
```csharp
static Quat LookDir(Vec3 direction)
```
Creates a rotation that describes looking towards a
direction. This is great for quickly describing facing behavior!
This rotation works best when applied to objects that face
Vec3.Forward in their resting/model space pose.

|  |  |
|--|--|
|Vec3 direction|Direction the rotation should be looking.             Doesn't need to be normalized.|
|RETURNS: Quat|A rotation that describes looking towards a direction.|
```csharp
static Quat LookDir(float x, float y, float z)
```
Creates a rotation that describes looking towards a
direction. This is great for quickly describing facing behavior!
This rotation works best when applied to objects that face
Vec3.Forward in their resting/model space pose.

|  |  |
|--|--|
|float x|X component of the direction the rotation should             be looking. Doesn't need to be normalized.|
|float y|Y component of the direction the rotation should             be looking. Doesn't need to be normalized.|
|float z|Z component of the direction the rotation should             be looking. Doesn't need to be normalized.|
|RETURNS: Quat|A rotation that describes looking towards a direction.|


## Quat.Difference
```csharp
static Quat Difference(Quat a, Quat b)
```
This gives a relative rotation between the first and
second quaternion rotations.
Remember that order is important here!

|  |  |
|--|--|
|Quat a|Starting rotation.|
|Quat b|Ending rotation.|
|RETURNS: Quat|A rotation that will take a point from rotation a, to rotation b.|


## Quat.FromAngles
```csharp
static Quat FromAngles(float pitchXDeg, float yawYDeg, float rollZDeg)
```
Creates a Roll/Pitch/Yaw rotation (applied in that
order) from the provided angles in degrees!

|  |  |
|--|--|
|float pitchXDeg|Pitch is rotation around the x axis,             measured in degrees.|
|float yawYDeg|Yaw is rotation around the y axis, measured             in degrees.|
|float rollZDeg|Roll is rotation around the z axis,              measured in degrees.|
|RETURNS: Quat|A quaternion representing the given Roll/Pitch/Yaw rotation!|
```csharp
static Quat FromAngles(Vec3 pitchYawRollDeg)
```
Creates a Roll/Pitch/Yaw rotation (applied in that
order) from the provided angles in degrees!

|  |  |
|--|--|
|Vec3 pitchYawRollDeg|Pitch, yaw, and roll stored as             X, Y, and Z in this Vector. Angle values are in degrees.|
|RETURNS: Quat|A quaternion representing the given Roll/Pitch/Yaw rotation!|


## Quat.Slerp
```csharp
static Quat Slerp(Quat a, Quat b, float slerp)
```
Spherical Linear intERPolation. Interpolates between two
quaternions! Both Quats should be normalized/unit quaternions, or
you may get unexpected results.

|  |  |
|--|--|
|Quat a|Start quaternion, should be normalized/unit              length.|
|Quat b|End quaternion, should be normalized/unit length.|
|float slerp|The interpolation amount! This'll be a if 0,              and b if 1. Unclamped.|
|RETURNS: Quat|A blend between the two quaternions!|


## Quat.ToString
```csharp
string ToString()
```
Mostly for debug purposes, this is a decent way to log or
inspect the quaternion in debug mode. Looks like "[x, y, z, w]"

|  |  |
|--|--|
|RETURNS: string|A string that looks like "[x, y, z, w]"|

# struct Ray

A position and a direction indicating a ray through space!
This is a great tool for intersection testing with geometrical
shapes.

## Ray.position

```csharp
Vec3 position
```

The position or origin point of the Ray.

## Ray.direction

```csharp
Vec3 direction
```

The direction the ray is facing, typically does not
require being a unit vector, or normalized direction.

## Ray.Ray
```csharp
void Ray(Vec3 position, Vec3 direction)
```
Basic initialization constructor! Just copies the
parameters into the fields.

|  |  |
|--|--|
|Vec3 position|The position or origin point of the Ray.|
|Vec3 direction|The direction the ray is facing,              typically does not require being a unit vector, or normalized              direction.|


## Ray.Intersect
```csharp
bool Intersect(Plane plane, Vec3& at)
```
Checks the intersection of this ray with a plane!

|  |  |
|--|--|
|Plane plane|Any plane you want to intersect with.|
|Vec3& at|An out parameter that will hold the intersection              point. If there's no intersection, this will be (0,0,0).|
|RETURNS: bool|True if there's an intersection, false if not. Refer to the 'at' parameter for intersection information!|
```csharp
bool Intersect(Sphere sphere, Vec3& at)
```
Checks the intersection of this ray with a sphere!

|  |  |
|--|--|
|Sphere sphere|Any Sphere you want to intersect with.|
|Vec3& at|An out parameter that will hold the closest              intersection point to the ray's origin. If there's no              intersection, this will be (0,0,0).|
|RETURNS: bool|True if intersection occurs, false if it doesn't. Refer to the 'at' parameter for intersection information!|
```csharp
bool Intersect(Bounds bounds, Vec3& at)
```
Checks the intersection of this ray with a bounding box!

|  |  |
|--|--|
|Bounds bounds|Any Bounds you want to intersect with.|
|Vec3& at|If the return is true, this point will be the              closest intersection point to the origin of the Ray. If there's              no intersection, this will be (0,0,0).|
|RETURNS: bool|True if intersection occurs, false if it doesn't. Refer to the 'at' parameter for intersection information!|
```csharp
bool Intersect(Mesh mesh, Ray& modelSpaceAt)
```
Checks the intersection point of this ray and a Mesh
with collision data stored on the CPU. A mesh without collision
data will always return false. Ray must be in model space,
intersection point will be in model space too. You can use the
inverse of the mesh's world transform matrix to bring the ray
into model space, see the example in the docs!

|  |  |
|--|--|
|Mesh mesh|A mesh containing collision data on the CPU.             You can check this with Mesh.KeepData.|
|Ray& modelSpaceAt|The intersection point and surface             direction of the ray and the mesh, if an intersection occurs.             This is in model space, and must be transformed back into world             space later. Direction is not guaranteed to be normalized,              especially if your own model->world transform contains scale/skew             in it.|
|RETURNS: bool|True if an intersection occurs, false otherwise!|
```csharp
bool Intersect(Mesh mesh, Ray& modelSpaceAt, UInt32& outStartInds)
```
Checks the intersection point of this ray and a Mesh
with collision data stored on the CPU. A mesh without collision
data will always return false. Ray must be in model space,
intersection point will be in model space too. You can use the
inverse of the mesh's world transform matrix to bring the ray
into model space, see the example in the docs!

|  |  |
|--|--|
|Mesh mesh|A mesh containing collision data on the CPU.             You can check this with Mesh.KeepData.|
|Ray& modelSpaceAt|The intersection point and surface             direction of the ray and the mesh, if an intersection occurs.             This is in model space, and must be transformed back into world             space later. Direction is not guaranteed to be normalized,              especially if your own model->world transform contains scale/skew             in it.|
|UInt32& outStartInds|The index of the first index of the triangle that was hit|
|RETURNS: bool|True if an intersection occurs, false otherwise!|
```csharp
bool Intersect(Model model, Ray& modelSpaceAt)
```
Checks the intersection point of this ray and the Solid
flagged Meshes in the Model's visual nodes. Ray must be in model
space, intersection point will be in model space too. You can use
the inverse of the mesh's world transform matrix to bring the ray
into model space, see the example in the docs!

|  |  |
|--|--|
|Model model|Any Model that may or may not contain Solid             flagged nodes, and Meshes with collision data.|
|Ray& modelSpaceAt|The intersection point and surface             direction of the ray and the mesh, if an intersection occurs.             This is in model space, and must be transformed back into world             space later. Direction is not guaranteed to be normalized,              especially if your own model->world transform contains scale/skew             in it.|
|RETURNS: bool|True if an intersection occurs, false otherwise!|


## Ray.Closest
```csharp
Vec3 Closest(Vec3 to)
```
Calculates the point on the Ray that's closest to the
given point! This can be in front of, or behind the ray's
starting position.

|  |  |
|--|--|
|Vec3 to|Any point in the same coordinate space as the              Ray.|
|RETURNS: Vec3|The point on the ray that's closest to the given point.|


## Ray.FromTo
```csharp
static Ray FromTo(Vec3 a, Vec3 b)
```
A convenience function that creates a ray from point a,
towards point b. Resulting direction is not normalized.

|  |  |
|--|--|
|Vec3 a|Ray starting point.|
|Vec3 b|Location the ray is pointing towards.|
|RETURNS: Ray|A ray from point a to point b. Not normalized.|


## Ray.At
```csharp
Vec3 At(float percent)
```
Gets a point along the ray! This is basically just
position + direction*percent. If Ray.direction is normalized,
then percent is functionally distance, and can be used to find
the point a certain distance out along the ray.

|  |  |
|--|--|
|float percent|How far along the ray should we get the              point at? This is in multiples of Ray.direction's magnitude. If             Ray.direction is normalized, this is functionally the distance.|
|RETURNS: Vec3|The point at position + direction*percent.|


## Ray.ToString
```csharp
string ToString()
```
Mostly for debug purposes, this is a decent way to log or
inspect the Ray in debug mode. Looks like "[position], [direction]"

|  |  |
|--|--|
|RETURNS: string|A string that looks like "[position], [direction]"|

# static class SKMath

This class contains some convenience math functions!
StereoKit typically uses floats instead of doubles, so you won't need
to cast to and from using these methods.

## SKMath.Pi

```csharp
static float Pi
```

The mathematical constant, Pi!

## SKMath.Tau

```csharp
static float Tau
```

Tau is 2*Pi, there are excellent arguments that Tau can
make certain formulas more readable!

## SKMath.Cos
```csharp
static float Cos(float f)
```
Same as Math.Cos

|  |  |
|--|--|
|RETURNS: float|Same as Math.Cos|


## SKMath.Sin
```csharp
static float Sin(float f)
```
Same as Math.Sin

|  |  |
|--|--|
|RETURNS: float|Same as Math.Sin|


## SKMath.Sqrt
```csharp
static float Sqrt(float f)
```
Same as Math.Sqrt

|  |  |
|--|--|
|RETURNS: float|Same as Math.Sqrt|


## SKMath.Exp
```csharp
static float Exp(float f)
```
Same as Math.Exp

|  |  |
|--|--|
|RETURNS: float|Same as Math.Exp|


## SKMath.Lerp
```csharp
static float Lerp(float a, float b, float t)
```
Blends (Linear Interpolation) between two scalars, based
on a 'blend' value, where 0 is a, and 1 is b. Doesn't clamp
percent for you.

|  |  |
|--|--|
|float a|First item in the blend, or '0.0' blend.|
|float b|Second item in the blend, or '1.0' blend.|
|float t|A blend value between 0 and 1. Can be outside             this range, it'll just interpolate outside of the a, b range.|
|RETURNS: float|An unclamped blend of a and b.|


## SKMath.AngleDist
```csharp
static float AngleDist(float a, float b)
```
Calculates the minimum angle 'distance' between two
angles. This covers wraparound cases like: the minimum distance
between 10 and 350 is 20.

|  |  |
|--|--|
|float a|First angle, in degrees.|
|float b|Second angle, in degrees.|
|RETURNS: float|Degrees 0-180, the minimum angle between a and b.|


## SKMath.Mod
```csharp
static int Mod(int x, int mod)
```
Modulus, works better than '%' for negative values.

|  |  |
|--|--|
|int x|Numerator|
|int mod|Denominator|
|RETURNS: int|x modulus mod|

# struct Sphere

Represents a sphere in 3D space! Composed of a center point
and a radius, can be used for raycasting, collision, visibility, and
other things!

## Sphere.center

```csharp
Vec3 center
```

Center of the sphere.

## Sphere.radius

```csharp
float radius
```

Distance from the center, to the surface of the sphere, in
meters. Half the diameter.

## Sphere.Diameter

```csharp
float Diameter{ get set }
```

Length of the line passing through the center from one side of
the sphere to the other, in meters. Twice the radius.

## Sphere.Sphere
```csharp
void Sphere(Vec3 center, float diameter)
```
Creates a sphere using a center point and a diameter!

|  |  |
|--|--|
|Vec3 center|Center of the sphere.|
|float diameter|Diameter is in meters. Twice the radius, the              distance from one side of the sphere to the other when drawing a line              through the center of the sphere.|


## Sphere.Intersect
```csharp
bool Intersect(Ray ray, Vec3& at)
```
Intersects a ray with this sphere, and finds if they intersect,
and if so, where that intersection is! This only finds the closest
intersection point to the origin of the ray.

|  |  |
|--|--|
|Ray ray|A ray to intersect with.|
|Vec3& at|An out parameter that will hold the closest intersection              point to the ray's origin. If there's not intersection, this will be (0,0,0).|
|RETURNS: bool|True if intersection occurs, false if it doesn't. Refer to the 'at' parameter for intersection information!|


## Sphere.Contains
```csharp
bool Contains(Vec3 point)
```
A fast check to see if the given point is contained in or on
a sphere!

|  |  |
|--|--|
|Vec3 point|A point in the same coordinate space as this sphere.|
|RETURNS: bool|True if in or on the sphere, false if outside.|

# static class Units

A collection of unit conversion constants! Multiply things by these
to convert them into different units.

## Units.deg2rad

```csharp
static float deg2rad
```

Degrees to radians, multiply degree values by this, and you get
radians! Like so: `180 * Units.deg2rad == 3.1415926536`, which is pi,
or 1 radian.

## Units.rad2deg

```csharp
static float rad2deg
```

Radians to degrees, multiply radian values by this, and you get
degrees! Like so: `PI * Units.rad2deg == 180`

## Units.cm2m

```csharp
static float cm2m
```

Converts centimeters to meters. There are 100cm in 1m. In StereoKit
1 unit is also 1 meter, so `25 * Units.cm2m == 0.25`, 25 centimeters is .25
meters/units.

## Units.mm2m

```csharp
static float mm2m
```

Converts millimeters to meters. There are 1000mm in 1m. In StereoKit
1 unit is 1 meter, so `250 * Units.mm2m == 0.25`, 250 millimeters is .25
meters/units.

## Units.m2cm

```csharp
static float m2cm
```

Converts meters to centimeters. There are 100cm in 1m, so this just
multiplies by 100.

## Units.m2mm

```csharp
static float m2mm
```

Converts meters to millimeters. There are 1000mm in 1m, so this just
multiplies by 1000.
# static class U

A shorthand class with unit multipliers. Helps make code a
little more terse on occasions!.

## U.cm

```csharp
static float cm
```

Converts centimeters to meters. There are 100cm in 1m. In StereoKit
1 unit is also 1 meter, so `25 * U.cm == 0.25`, 25 centimeters is .25
meters/units.

## U.mm

```csharp
static float mm
```

Converts millimeters to meters. There are 1000mm in 1m. In StereoKit
1 unit is 1 meter, so `250 * Units.mm2m == 0.25`, 250 millimeters is .25
meters/units.

## U.m

```csharp
static float m
```

StereoKit's default unit is meters, but sometimes it's
nice to be explicit!

## U.km

```csharp
static float km
```

Converts meters to kilometers. There are 1000m in 1km,
so this just multiplies by 1000.
# static class V

This is a collection of vector initialization shorthands.
Since math can often get a little long to write, saving a few
characters here and there can make a difference in readability. This
also comes with some swizzles to make things even shorter! I also
don't love the 'new' keyword on Vectors, and this eliminates that.

For example: instead of `new Vec3(2.0f, 2.0f, 2.0f)` or even
`Vec3.One * 2.0f`, you could write `V.XXX(2.0f)`

## V.XY
```csharp
static Vec2 XY(float x, float y)
```
Creates a Vec2, this is a straight alternative to
`new Vec2(x, y)`

|  |  |
|--|--|
|float x|X component of the Vector|
|float y|Y component of the Vector|
|RETURNS: Vec2|A Vec2(x, y)|


## V.XX
```csharp
static Vec2 XX(float x)
```
Creates a Vec2 where both components are the same value.
This is the same as `new Vec2(x, x)`

|  |  |
|--|--|
|float x|Both X and Y components will have this value.|
|RETURNS: Vec2|A Vec2(x, x)|


## V.XYZ
```csharp
static Vec3 XYZ(float x, float y, float z)
```
Creates a Vec3, this is a straight alternative to
`new Vec3(x, y, z)`

|  |  |
|--|--|
|float x|X component of the Vector|
|float y|Y component of the Vector|
|float z|Z component of the Vector|
|RETURNS: Vec3|A Vec3(x, y, z)|


## V.XY0
```csharp
static Vec3 XY0(float x, float y)
```
Creates a Vec3, this is a straight alternative to
`new Vec3(x, y, 0)`

|  |  |
|--|--|
|float x|X component of the Vector|
|float y|Y component of the Vector|
|RETURNS: Vec3|A Vec3(x, y, 0)|


## V.X0Z
```csharp
static Vec3 X0Z(float x, float z)
```
Creates a Vec3, this is a straight alternative to
`new Vec3(x, 0, z)`

|  |  |
|--|--|
|float x|X component of the Vector|
|float z|Z component of the Vector|
|RETURNS: Vec3|A Vec3(x, 0, z)|


## V.XXX
```csharp
static Vec3 XXX(float x)
```
Creates a Vec3 where all components are the same value.
This is the same as `new Vec3(x, x, x)`

|  |  |
|--|--|
|float x|X, Y and Z components will have this value.|
|RETURNS: Vec3|A Vec3(x, x, x)|


## V.XYZW
```csharp
static Vec4 XYZW(float x, float y, float z, float w)
```
Creates a Vec4, this is a straight alternative to
`new Vec4(x, y, z, w)`

|  |  |
|--|--|
|float x|X component of the Vector|
|float y|Y component of the Vector|
|float z|Z component of the Vector|
|float w|W component of the Vector|
|RETURNS: Vec4|A Vec4(x, y, z, w)|


## V.XXXX
```csharp
static Vec4 XXXX(float x)
```
Creates a Vec4 where all components are the same value.
This is the same as `new Vec4(x, x, x, x)`

|  |  |
|--|--|
|float x|X, Y, Z and W components will have this value.|
|RETURNS: Vec4|A Vec4(x, x, x, x)|

# struct Vec2

A vector with 2 components: x and y. This can represent a
point in 2D space, a directional vector, or any other sort of value
with 2 dimensions to it!

## Vec2.v

```csharp
Vector2 v
```

The internal, wrapped System.Numerics type. This can be
nice to have around so you can pass its fields as 'ref', which
you can't do with properties. You won't often need this, as
implicit conversions to System.Numerics types are also
provided.

## Vec2.x

```csharp
float x{ get set }
```

X component.

## Vec2.y

```csharp
float y{ get set }
```

Y component.

## Vec2.Vec2
```csharp
void Vec2(float x, float y)
```
A basic constructor, just copies the values in!

|  |  |
|--|--|
|float x|X component of the vector.|
|float y|Y component of the vector.|
```csharp
void Vec2(float xy)
```
A short hand constructor, just sets all values as the same!

|  |  |
|--|--|
|float xy|X and Y component of the vector.|


## Vec2.Implicit Conversions
```csharp
static Vec2 Implicit Conversions(Vector2 v)
```
Allows implicit conversion from System.Numerics.Vector2
to StereoKit.Vec2.

|  |  |
|--|--|
|Vector2 v|Any System.Numerics Vector2.|
|RETURNS: Vec2|A StereoKit compatible vector.|
```csharp
static Vector2 Implicit Conversions(Vec2 v)
```
Allows implicit conversion from StereoKit.Vec2 to
System.Numerics.Vector2

|  |  |
|--|--|
|Vec2 v|Any StereoKit.Vec2.|
|RETURNS: Vector2|A System.Numerics compatible vector.|


## Vec2.+
```csharp
static Vec2 +(Vec2 a, Vec2 b)
```
Adds matching components together. Commutative.

|  |  |
|--|--|
|Vec2 a|Any vector.|
|Vec2 b|Any vector.|
|RETURNS: Vec2|A new vector from the added components.|


## Vec2.-
```csharp
static Vec2 -(Vec2 a, Vec2 b)
```
Subtracts matching components from eachother. Not
commutative.

|  |  |
|--|--|
|Vec2 a|Any vector.|
|Vec2 b|Any vector.|
|RETURNS: Vec2|A new vector from the subtracted components.|


## Vec2.*
```csharp
static Vec2 *(Vec2 a, Vec2 b)
```
A component-wise vector multiplication, same thing as
a non-uniform scale. NOT a dot product! Commutative.

|  |  |
|--|--|
|Vec2 a|Any vector.|
|Vec2 b|Any vector.|
|RETURNS: Vec2|A new vector a scaled by b.|
```csharp
static Vec2 *(Vec2 a, float b)
```
A scalar vector multiplication.

|  |  |
|--|--|
|Vec2 a|Any vector.|
|float b|Any scalar.|
|RETURNS: Vec2|A new vector a scaled by b.|
```csharp
static Vec2 *(float a, Vec2 b)
```
A scalar vector multiplication.

|  |  |
|--|--|
|float a|Any scalar.|
|Vec2 b|Any vector.|
|RETURNS: Vec2|A new vector a scaled by b.|


## Vec2./
```csharp
static Vec2 /(Vec2 a, Vec2 b)
```
A component-wise vector division. Not commutative

|  |  |
|--|--|
|Vec2 a|Any vector.|
|Vec2 b|Any vector.|
|RETURNS: Vec2|A new vector a divided by b.|
```csharp
static Vec2 /(Vec2 a, float b)
```
A scalar vector division.

|  |  |
|--|--|
|Vec2 a|Any vector.|
|float b|Any scalar.|
|RETURNS: Vec2|A new vector a divided by b.|


## Vec2.-
```csharp
static Vec2 -(Vec2 a)
```
Vector negation, returns a vector where each component has
been negated.

|  |  |
|--|--|
|Vec2 a|Any vector.|
|RETURNS: Vec2|A vector where each component has been negated.|


## Vec2.Zero

```csharp
static Vec2 Zero
```

A Vec2 with all components at zero, this is the same as
`new Vec2(0,0)`.

## Vec2.One

```csharp
static Vec2 One
```

A Vec2 with all components at one, this is the same as
`new Vec2(1,1)`.

## Vec2.UnitX

```csharp
static Vec2 UnitX
```

A normalized Vector that points down the X axis, this is
the same as `new Vec2(1,0)`.

## Vec2.UnitY

```csharp
static Vec2 UnitY
```

A normalized Vector that points down the Y axis, this is
the same as `new Vec2(0,1)`.

## Vec2.YX

```csharp
Vec2 YX{ get }
```

A transpose swizzle property, returns (y,x)

## Vec2.XY0

```csharp
Vec3 XY0{ get }
```

Promotes this Vec2 to a Vec3, using 0 for the Z axis.

## Vec2.X0Y

```csharp
Vec3 X0Y{ get }
```

Promotes this Vec2 to a Vec3, using 0 for the Y axis.

## Vec2.Magnitude

```csharp
float Magnitude{ get }
```

Magnitude is the length of the vector! Or the distance
from the origin to this point. Uses Math.Sqrt, so it's not dirt
cheap or anything.

## Vec2.Length

```csharp
float Length{ get }
```

This is the length of the vector! Or the distance
from the origin to this point. Uses Math.Sqrt, so it's not dirt
cheap or anything.

## Vec2.MagnitudeSq

```csharp
float MagnitudeSq{ get }
```

This is the squared magnitude of the vector! It skips
the Sqrt call, and just gives you the squared version for speedy
calculations that can work with it squared.

## Vec2.LengthSq

```csharp
float LengthSq{ get }
```

This is the squared length/magnitude of the vector! It
skips the Sqrt call, and just gives you the squared version for
speedy calculations that can work with it squared.

## Vec2.Normalized

```csharp
Vec2 Normalized{ get }
```

Creates a normalized vector (vector with a length of 1)
from the current vector. Will not work properly if the vector has
a length of zero.

## Vec2.Dot
```csharp
static float Dot(Vec2 a, Vec2 b)
```
The dot product is an extremely useful operation! One
major use is to determine how similar two vectors are. If the
vectors are Unit vectors (magnitude/length of 1), then the result
will be 1 if the vectors are the same, -1 if they're opposite,
and a gradient in-between with 0 being perpendicular. See
[Freya Holmer's excellent visualization](https://twitter.com/FreyaHolmer/status/1200807790580768768)
of this concept

|  |  |
|--|--|
|Vec2 a|First vector.|
|Vec2 b|Second vector.|
|RETURNS: float|The dot product!|


## Vec2.FromAngle
```csharp
static Vec2 FromAngle(float degrees)
```
Creates a vector pointing in the direction of the angle,
with a length of 1. Angles are counter-clockwise, and start from
(1,0), so an angle of 90 will be (0,1).

|  |  |
|--|--|
|float degrees|Counter-clockwise angle from (1,0), in             degrees.|
|RETURNS: Vec2|A unit vector (length of 1), pointing towards degrees.|


## Vec2.Angle
```csharp
float Angle()
```
Returns the counter-clockwise degrees from [1,0].
Resulting value is between 0 and 360. Vector does not need to be
normalized.

|  |  |
|--|--|
|RETURNS: float|Counter-clockwise angle of the vector in degrees from [1,0], as a value between 0 and 360.|


## Vec2.Normalize
```csharp
void Normalize()
```
Turns this vector into a normalized vector (vector with
a length of 1) from the current vector. Will not work properly if
the vector has a length of zero.


## Vec2.InRadius
```csharp
bool InRadius(Vec2 pt, float radius)
```
Checks if a point is within a certain radius of this one.
This is an easily readable shorthand of the squared distance check.

|  |  |
|--|--|
|Vec2 pt|The point to check against.|
|float radius|The distance to check against.|
|RETURNS: bool|True if the points are within radius of each other, false not.|
```csharp
static bool InRadius(Vec2 a, Vec2 b, float radius)
```
Checks if two points are within a certain radius of
each other. This is an easily readable shorthand of the squared
distance check.

|  |  |
|--|--|
|Vec2 a|The first point.|
|Vec2 b|And the second point!|
|float radius|The distance to check against.|
|RETURNS: bool|True if a and b are within radius of each other, false if not.|


## Vec2.AngleBetween
```csharp
static float AngleBetween(Vec2 a, Vec2 b)
```
Calculates a signed angle between two vectors in degrees!
Sign will be positive if B is counter-clockwise (left) of A, and
negative if B is clockwise (right) of A. Vectors do not need to be
normalized.
NOTE: Since this will return a positive or negative angle, order of
parameters matters!

|  |  |
|--|--|
|Vec2 a|The first, initial vector, A. Does not need to be             normalized.|
|Vec2 b|The second vector, B, that we're finding the              angle to. Does not need to be normalized.|
|RETURNS: float|a signed angle between two vectors in degrees! Sign will be positive if B is counter-clockwise (left) of A, and negative if B is clockwise (right) of A.|


## Vec2.Distance
```csharp
static float Distance(Vec2 a, Vec2 b)
```
Calculates the distance between two points in space!
Make sure they're in the same coordinate space! Uses a Sqrt, so
it's not blazing fast, prefer DistanceSq when possible.

|  |  |
|--|--|
|Vec2 a|The first point.|
|Vec2 b|And the second point!|
|RETURNS: float|Distance between the two points.|


## Vec2.DistanceSq
```csharp
static float DistanceSq(Vec2 a, Vec2 b)
```
Calculates the distance between two points in space, but
leaves them squared! Make sure they're in the same coordinate
space! This is a fast function :)

|  |  |
|--|--|
|Vec2 a|The first point.|
|Vec2 b|And the second point!|
|RETURNS: float|Distance between the two points, but squared!|


## Vec2.Lerp
```csharp
static Vec2 Lerp(Vec2 a, Vec2 b, float blend)
```
Blends (Linear Interpolation) between two vectors, based
on a 'blend' value, where 0 is a, and 1 is b. Doesn't clamp
percent for you.

|  |  |
|--|--|
|Vec2 a|First item in the blend, or '0.0' blend.|
|Vec2 b|Second item in the blend, or '1.0' blend.|
|float blend|A blend value between 0 and 1. Can be outside             this range, it'll just interpolate outside of the a, b range.|
|RETURNS: Vec2|An unclamped blend of a and b.|


## Vec2.Max
```csharp
static Vec2 Max(Vec2 a, Vec2 b)
```
Returns a vector where each elements is the maximum
value for each corresponding pair.

|  |  |
|--|--|
|Vec2 a|Order isn't important here.|
|Vec2 b|Order isn't important here.|
|RETURNS: Vec2|The maximum value for each corresponding vector pair.|


## Vec2.Min
```csharp
static Vec2 Min(Vec2 a, Vec2 b)
```
Returns a vector where each elements is the minimum
value for each corresponding pair.

|  |  |
|--|--|
|Vec2 a|Order isn't important here.|
|Vec2 b|Order isn't important here.|
|RETURNS: Vec2|The minimum value for each corresponding vector pair.|


## Vec2.ToString
```csharp
string ToString()
```
Mostly for debug purposes, this is a decent way to log or
inspect the vector in debug mode. Looks like "[x, y]"

|  |  |
|--|--|
|RETURNS: string|A string that looks like "[x, y]"|

# struct Vec3

A vector with 3 components: x, y, z. This can represent a
point in space, a directional vector, or any other sort of value with
3 dimensions to it!

StereoKit uses a right-handed coordinate system, where +x is to the
right, +y is upwards, and -z is forward.

## Vec3.v

```csharp
Vector3 v
```

The internal, wrapped System.Numerics type. This can be
nice to have around so you can pass its fields as 'ref', which
you can't do with properties. You won't often need this, as
implicit conversions to System.Numerics types are also
provided.

## Vec3.x

```csharp
float x{ get set }
```

X component.

## Vec3.y

```csharp
float y{ get set }
```

Y component.

## Vec3.z

```csharp
float z{ get set }
```

Z component.

## Vec3.Vec3
```csharp
void Vec3(float x, float y, float z)
```
Creates a vector from x, y, and z values! StereoKit uses
a right-handed metric coordinate system, where +x is to the
right, +y is upwards, and -z is forward.

|  |  |
|--|--|
|float x|The x axis.|
|float y|The y axis.|
|float z|The z axis.|
```csharp
void Vec3(float xyz)
```
Creates a vector with all values the same! StereoKit uses
a right-handed metric coordinate system, where +x is to the
right, +y is upwards, and -z is forward.

|  |  |
|--|--|
|float xyz|The x,y,and z axis.|


## Vec3.Implicit Conversions
```csharp
static Vec3 Implicit Conversions(Vector3 v)
```
Allows implicit conversion from System.Numerics.Vector3
to StereoKit.Vec3.

|  |  |
|--|--|
|Vector3 v|Any System.Numerics Vector3.|
|RETURNS: Vec3|A StereoKit compatible vector.|
```csharp
static Vector3 Implicit Conversions(Vec3 v)
```
Allows implicit conversion from StereoKit.Vec3 to
System.Numerics.Vector3

|  |  |
|--|--|
|Vec3 v|Any StereoKit.Vec3.|
|RETURNS: Vector3|A System.Numerics compatible vector.|


## Vec3.+
```csharp
static Vec3 +(Vec3 a, Vec3 b)
```
Adds matching components together. Commutative.

|  |  |
|--|--|
|Vec3 a|Any vector.|
|Vec3 b|Any vector.|
|RETURNS: Vec3|A new vector from the added components.|


## Vec3.-
```csharp
static Vec3 -(Vec3 a, Vec3 b)
```
Subtracts matching components from eachother. Not
commutative.

|  |  |
|--|--|
|Vec3 a|Any vector.|
|Vec3 b|Any vector.|
|RETURNS: Vec3|A new vector from the subtracted components.|


## Vec3.*
```csharp
static Vec3 *(Vec3 a, Vec3 b)
```
A component-wise vector multiplication, same thing as
a non-uniform scale. NOT a dot or cross product! Commutative.

|  |  |
|--|--|
|Vec3 a|Any vector.|
|Vec3 b|Any vector.|
|RETURNS: Vec3|A new vector a scaled by b.|
```csharp
static Vec3 *(Vec3 a, float b)
```
A scalar vector multiplication.

|  |  |
|--|--|
|Vec3 a|Any vector.|
|float b|Any scalar.|
|RETURNS: Vec3|A new vector a scaled by b.|
```csharp
static Vec3 *(float a, Vec3 b)
```
A scalar vector multiplication.

|  |  |
|--|--|
|float a|Any scalar.|
|Vec3 b|Any vector.|
|RETURNS: Vec3|A new vector a scaled by b.|


## Vec3./
```csharp
static Vec3 /(Vec3 a, Vec3 b)
```
A component-wise vector division. Not commutative

|  |  |
|--|--|
|Vec3 a|Any vector.|
|Vec3 b|Any vector.|
|RETURNS: Vec3|A new vector a divided by b.|
```csharp
static Vec3 /(Vec3 a, float b)
```
A scalar vector division.

|  |  |
|--|--|
|Vec3 a|Any vector.|
|float b|Any scalar.|
|RETURNS: Vec3|A new vector a divided by b.|


## Vec3.-
```csharp
static Vec3 -(Vec3 a)
```
Vector negation, returns a vector where each component has
been negated.

|  |  |
|--|--|
|Vec3 a|Any vector.|
|RETURNS: Vec3|A vector where each component has been negated.|


## Vec3.Zero

```csharp
static Vec3 Zero
```

Shorthand for a vector where all values are 0! Same as
`new Vec3(0,0,0)`.

## Vec3.One

```csharp
static Vec3 One
```

Shorthand for a vector where all values are 1! Same as
`new Vec3(1,1,1)`.

## Vec3.Up

```csharp
static Vec3 Up
```

A vector representing the up axis. In StereoKit, this is
the same as `new Vec3(0,1,0)`.

## Vec3.Forward

```csharp
static Vec3 Forward
```

StereoKit uses a right-handed coordinate system, which
means that forward is looking down the -Z axis! This value is the
same as `new Vec3(0,0,-1)`. This is NOT the same as UnitZ!

## Vec3.Right

```csharp
static Vec3 Right
```

When looking forward, this is the direction to the
right! In StereoKit, this is the same as `new Vec3(1,0,0)`

## Vec3.UnitX

```csharp
static Vec3 UnitX
```

A normalized Vector that points down the X axis, this is
the same as `new Vec3(1,0,0)`.

## Vec3.UnitY

```csharp
static Vec3 UnitY
```

A normalized Vector that points down the Y axis, this is
the same as `new Vec3(0,1,0)`.

## Vec3.UnitZ

```csharp
static Vec3 UnitZ
```

A normalized Vector that points down the Z axis, this is
the same as `new Vec3(0,0,1)`. This is NOT the same as Forward!

## Vec3.XY

```csharp
Vec2 XY{ get set }
```

This extracts a Vec2 from the X and Y axes.

## Vec3.YZ

```csharp
Vec2 YZ{ get set }
```

This extracts a Vec2 from the Y and Z axes.

## Vec3.XZ

```csharp
Vec2 XZ{ get set }
```

This extracts a Vec2 from the X and Z axes.

## Vec3.X0Z

```csharp
Vec3 X0Z{ get }
```

This returns a Vec3 that has been flattened to 0 on the
Y axis. No other changes are made.

## Vec3.XY0

```csharp
Vec3 XY0{ get }
```

This returns a Vec3 that has been flattened to 0 on the
Z axis. No other changes are made.

## Vec3.Magnitude

```csharp
float Magnitude{ get }
```

Magnitude is the length of the vector! The distance from
the origin to this point. Uses Math.Sqrt, so it's not dirt cheap
or anything.

## Vec3.Length

```csharp
float Length{ get }
```

This is the length, or magnitude of the vector! The
distance from the origin to this point. Uses Math.Sqrt, so it's
not dirt cheap or anything.

## Vec3.MagnitudeSq

```csharp
float MagnitudeSq{ get }
```

This is the squared magnitude of the vector! It skips
the Sqrt call, and just gives you the squared version for speedy
calculations that can work with it squared.

## Vec3.LengthSq

```csharp
float LengthSq{ get }
```

This is the squared length of the vector! It skips the
Sqrt call, and just gives you the squared version for speedy
calculations that can work with it squared.

## Vec3.Normalized

```csharp
Vec3 Normalized{ get }
```

Creates a normalized vector (vector with a length of 1)
from the current vector. Will not work properly if the vector has
a length of zero.

## Vec3.Dot
```csharp
static float Dot(Vec3 a, Vec3 b)
```
The dot product is an extremely useful operation! One
major use is to determine how similar two vectors are. If the
vectors are Unit vectors (magnitude/length of 1), then the result
will be 1 if the vectors are the same, -1 if they're opposite,
and a gradient in-between with 0 being perpendicular. See
[Freya Holmer's excellent visualization](https://twitter.com/FreyaHolmer/status/1200807790580768768)
of this concept

|  |  |
|--|--|
|Vec3 a|First vector.|
|Vec3 b|Second vector.|
|RETURNS: float|The dot product!|


## Vec3.Cross
```csharp
static Vec3 Cross(Vec3 a, Vec3 b)
```
The cross product of two vectors!

|  |  |
|--|--|
|Vec3 a|First vector!|
|Vec3 b|Second vector!|
|RETURNS: Vec3|Result is -not- a unit vector, even if both 'a' and 'b' are unit vectors.|


## Vec3.Normalize
```csharp
void Normalize()
```
Turns this vector into a normalized vector (vector with
a length of 1) from the current vector. Will not work properly if
the vector has a length of zero.


## Vec3.InRadius
```csharp
bool InRadius(Vec3 pt, float radius)
```
Checks if a point is within a certain radius of this one.
This is an easily readable shorthand of the squared distance check.

|  |  |
|--|--|
|Vec3 pt|The point to check against.|
|float radius|The distance to check against.|
|RETURNS: bool|True if the points are within radius of each other, false not.|
```csharp
static bool InRadius(Vec3 a, Vec3 b, float radius)
```
Checks if two points are within a certain radius of
each other. This is an easily readable shorthand of the squared
distance check.

|  |  |
|--|--|
|Vec3 a|The first point.|
|Vec3 b|And the second point!|
|float radius|The distance to check against.|
|RETURNS: bool|True if a and b are within radius of each other, false if not.|


## Vec3.Distance
```csharp
static float Distance(Vec3 a, Vec3 b)
```
Calculates the distance between two points in space!
Make sure they're in the same coordinate space! Uses a Sqrt, so
it's not blazing fast, prefer DistanceSq when possible.

|  |  |
|--|--|
|Vec3 a|The first point.|
|Vec3 b|And the second point!|
|RETURNS: float|Distance between the two points.|


## Vec3.DistanceSq
```csharp
static float DistanceSq(Vec3 a, Vec3 b)
```
Calculates the distance between two points in space, but
leaves them squared! Make sure they're in the same coordinate
space! This is a fast function :)

|  |  |
|--|--|
|Vec3 a|The first point.|
|Vec3 b|And the second point!|
|RETURNS: float|Distance between the two points, but squared!|


## Vec3.AngleXZ
```csharp
static Vec3 AngleXZ(float angleDeg, float y)
```
Creates a vector that points out at the given 2D angle!
This creates the vector on the XZ plane, and allows you to
specify a constant y value.

|  |  |
|--|--|
|float angleDeg|Angle in degrees, starting from (1,0) at             0, and continuing to (0,1) at 90.|
|float y|A constant value you can assign to the resulting             vector's y component.|
|RETURNS: Vec3|A Vector pointing at the given angle! If y is zero, this will be a normalized vector (vector with a length of 1).|


## Vec3.AngleXY
```csharp
static Vec3 AngleXY(float angleDeg, float z)
```
Creates a vector that points out at the given 2D angle!
This creates the vector on the XY plane, and allows you to
specify a constant z value.

|  |  |
|--|--|
|float angleDeg|Angle in degrees, starting from (1,0) at             0, and continuing to (0,1) at 90.|
|float z|A constant value you can assign to the resulting             vector's z component.|
|RETURNS: Vec3|A vector pointing at the given angle! If z is zero, this will be a normalized vector (vector with a length of 1).|


## Vec3.AngleBetween
```csharp
static float AngleBetween(Vec3 a, Vec3 b)
```
Calculates the angle between two vectors in degrees!
Vectors do not need to be normalized, and the result will always be
positive.

|  |  |
|--|--|
|Vec3 a|The first, initial vector, A. Does not need to be             normalized.|
|Vec3 b|The second vector, B, that we're finding the             angle to. Does not need to be normalized.|
|RETURNS: float|A positive angle between two vectors in degrees!|


## Vec3.PerpendicularRight
```csharp
static Vec3 PerpendicularRight(Vec3 forward, Vec3 up)
```
Exactly the same as Vec3.Cross, but has some naming
mnemonics for getting the order right when trying to find a
perpendicular vector using the cross product. This'll also make
it more obvious to read if that's what you're actually going for
when crossing vectors!

If you consider a forward vector and an up vector, then the
direction to the right is pretty trivial to imagine in relation
to those vectors!

|  |  |
|--|--|
|Vec3 forward|What way are you facing?|
|Vec3 up|What direction is up?|
|RETURNS: Vec3|Your direction to the right! Result is -not- a unit vector, even if both 'forward' and 'up' are unit vectors.|


## Vec3.Lerp
```csharp
static Vec3 Lerp(Vec3 a, Vec3 b, float blend)
```
Blends (Linear Interpolation) between two vectors, based
on a 'blend' value, where 0 is a, and 1 is b. Doesn't clamp
percent for you.

|  |  |
|--|--|
|Vec3 a|First item in the blend, or '0.0' blend.|
|Vec3 b|Second item in the blend, or '1.0' blend.|
|float blend|A blend value between 0 and 1. Can be outside             this range, it'll just interpolate outside of the a, b range.|
|RETURNS: Vec3|An unclamped blend of a and b.|


## Vec3.Max
```csharp
static Vec3 Max(Vec3 a, Vec3 b)
```
Returns a vector where each elements is the maximum
value for each corresponding pair.

|  |  |
|--|--|
|Vec3 a|Order isn't important here.|
|Vec3 b|Order isn't important here.|
|RETURNS: Vec3|The maximum value for each corresponding vector pair.|


## Vec3.Min
```csharp
static Vec3 Min(Vec3 a, Vec3 b)
```
Returns a vector where each elements is the minimum
value for each corresponding pair.

|  |  |
|--|--|
|Vec3 a|Order isn't important here.|
|Vec3 b|Order isn't important here.|
|RETURNS: Vec3|The minimum value for each corresponding vector pair.|


## Vec3.ToString
```csharp
string ToString()
```
Mostly for debug purposes, this is a decent way to log or
inspect the vector in debug mode. Looks like "[x, y, z]"

|  |  |
|--|--|
|RETURNS: string|A string that looks like "[x, y, z]"|

# struct Vec4

A vector with 4 components: x, y, z, and w. Can be useful
for things like shaders, where the registers are aligned to 4 float
vectors.

This is a wrapper on System.Numerics.Vector4, so it's SIMD optimized,
and can be cast to and from implicitly.

## Vec4.v

```csharp
Vector4 v
```

The internal, wrapped System.Numerics type. This can be
nice to have around so you can pass its fields as 'ref', which
you can't do with properties. You won't often need this, as
implicit conversions to System.Numerics types are also
provided.

## Vec4.x

```csharp
float x{ get set }
```

X component.

## Vec4.y

```csharp
float y{ get set }
```

Y component.

## Vec4.z

```csharp
float z{ get set }
```

Z component.

## Vec4.w

```csharp
float w{ get set }
```

W component.

## Vec4.UnitX

```csharp
static Vec4 UnitX
```

A normalized Vector that points down the X axis, this is
the same as `new Vec4(1,0,0,0)`.

## Vec4.UnitY

```csharp
static Vec4 UnitY
```

A normalized Vector that points down the Y axis, this is
the same as `new Vec4(0,1,0,0)`.

## Vec4.UnitZ

```csharp
static Vec4 UnitZ
```

A normalized Vector that points down the Z axis, this is
the same as `new Vec4(0,1,0,0)`.

## Vec4.UnitW

```csharp
static Vec4 UnitW
```

A normalized Vector that points down the W axis, this is
the same as `new Vec4(0,1,0,0)`.

## Vec4.XY

```csharp
Vec2 XY{ get set }
```

This extracts a Vec2 from the X and Y axes.

## Vec4.YZ

```csharp
Vec2 YZ{ get set }
```

This extracts a Vec2 from the Y and Z axes.

## Vec4.ZW

```csharp
Vec2 ZW{ get set }
```

This extracts a Vec2 from the Z and W axes.

## Vec4.XZ

```csharp
Vec2 XZ{ get set }
```

This extracts a Vec2 from the X and Z axes.

## Vec4.XYZ

```csharp
Vec3 XYZ{ get set }
```

This extracts a Vec3 from the X, Y, and Z axes.

## Vec4.Quat

```csharp
Quat Quat{ get }
```

A Vec4 and a Quat are only really different by name and
purpose. So, if you need to do Quat math with your Vec4, or visa
versa, who am I to judge?

## Vec4.Vec4
```csharp
void Vec4(float x, float y, float z, float w)
```
A basic constructor, just copies the values in!

|  |  |
|--|--|
|float x|X component of the vector.|
|float y|Y component of the vector.|
|float z|Z component of the vector.|
|float w|W component of the vector.|
```csharp
void Vec4(float xyzw)
```
A basic constructor, just copies the value in as the x, y,
z and w components!

|  |  |
|--|--|
|float xyzw|X,Y,Z,and W component of the vector.|
```csharp
void Vec4(Vec3 xyz, float w)
```
A short hand constructor, just copies the values in!

|  |  |
|--|--|
|Vec3 xyz|X, Y and Z components of the vector.|
|float w|W component of the vector.|
```csharp
void Vec4(Vec2 xy, Vec2 zw)
```
A basic constructor, just copies the values in!

|  |  |
|--|--|
|Vec2 xy|X and Y components of the vector.|
|Vec2 zw|Z and W components of the vector.|


## Vec4.Implicit Conversions
```csharp
static Vec4 Implicit Conversions(Vector4 v)
```
Allows implicit conversion from System.Numerics.Vector4
to StereoKit.Vec4.

|  |  |
|--|--|
|Vector4 v|Any System.Numerics Vector4.|
|RETURNS: Vec4|A StereoKit compatible vector.|
```csharp
static Vector4 Implicit Conversions(Vec4 v)
```
Allows implicit conversion from StereoKit.Vec4 to
System.Numerics.Vector4.

|  |  |
|--|--|
|Vec4 v|Any StereoKit.Vec4.|
|RETURNS: Vector4|A System.Numerics compatible vector.|


## Vec4.+
```csharp
static Vec4 +(Vec4 a, Vec4 b)
```
Adds matching components together. Commutative.

|  |  |
|--|--|
|Vec4 a|Any vector.|
|Vec4 b|Any vector.|
|RETURNS: Vec4|A new vector from the added components.|


## Vec4.-
```csharp
static Vec4 -(Vec4 a, Vec4 b)
```
Subtracts matching components from eachother. Not
commutative.

|  |  |
|--|--|
|Vec4 a|Any vector.|
|Vec4 b|Any vector.|
|RETURNS: Vec4|A new vector from the subtracted components.|


## Vec4.*
```csharp
static Vec4 *(Vec4 a, Vec4 b)
```
A component-wise vector multiplication, same thing as
a non-uniform scale. NOT a dot product! Commutative.

|  |  |
|--|--|
|Vec4 a|Any vector.|
|Vec4 b|Any vector.|
|RETURNS: Vec4|A new vector a scaled by b.|
```csharp
static Vec4 *(Vec4 a, float b)
```
A scalar vector multiplication.

|  |  |
|--|--|
|Vec4 a|Any vector.|
|float b|Any scalar.|
|RETURNS: Vec4|A new vector a scaled by b.|
```csharp
static Vec4 *(float a, Vec4 b)
```
A scalar vector multiplication.

|  |  |
|--|--|
|float a|Any scalar.|
|Vec4 b|Any vector.|
|RETURNS: Vec4|A new vector a scaled by b.|


## Vec4./
```csharp
static Vec4 /(Vec4 a, Vec4 b)
```
A component-wise vector division. Not commutative

|  |  |
|--|--|
|Vec4 a|Any vector.|
|Vec4 b|Any vector.|
|RETURNS: Vec4|A new vector a divided by b.|
```csharp
static Vec4 /(Vec4 a, float b)
```
A scalar vector division.

|  |  |
|--|--|
|Vec4 a|Any vector.|
|float b|Any vector.|
|RETURNS: Vec4|A new vector a divided by b.|


## Vec4.-
```csharp
static Vec4 -(Vec4 a)
```
Vector negation, returns a vector where each component has
been negated.

|  |  |
|--|--|
|Vec4 a|Any vector.|
|RETURNS: Vec4|A vector where each component has been negated.|


## Vec4.Dot
```csharp
static float Dot(Vec4 a, Vec4 b)
```
What's a dot product do for 4D vectors, you might ask?
Well, I'm no mathematician, so hopefully you are! I've never used
it before. Whatever you're doing with this function, it's SIMD
fast!

|  |  |
|--|--|
|Vec4 a|First vector.|
|Vec4 b|Second vector.|
|RETURNS: float|The dot product!|


## Vec4.Lerp
```csharp
static Vec4 Lerp(Vec4 a, Vec4 b, float blend)
```
Blends (Linear Interpolation) between two vectors, based
on a 'blend' value, where 0 is a, and 1 is b. Doesn't clamp
percent for you.

|  |  |
|--|--|
|Vec4 a|First item in the blend, or '0.0' blend.|
|Vec4 b|Second item in the blend, or '1.0' blend.|
|float blend|A blend value between 0 and 1. Can be outside             this range, it'll just interpolate outside of the a, b range.|
|RETURNS: Vec4|An unclamped blend of a and b.|


## Vec4.Max
```csharp
static Vec4 Max(Vec4 a, Vec4 b)
```
Returns a vector where each elements is the maximum
value for each corresponding pair.

|  |  |
|--|--|
|Vec4 a|Order isn't important here.|
|Vec4 b|Order isn't important here.|
|RETURNS: Vec4|The maximum value for each corresponding vector pair.|


## Vec4.Min
```csharp
static Vec4 Min(Vec4 a, Vec4 b)
```
Returns a vector where each elements is the minimum
value for each corresponding pair.

|  |  |
|--|--|
|Vec4 a|Order isn't important here.|
|Vec4 b|Order isn't important here.|
|RETURNS: Vec4|The minimum value for each corresponding vector pair.|


## Vec4.ToString
```csharp
string ToString()
```
Mostly for debug purposes, this is a decent way to log or
inspect the vector in debug mode. Looks like "[x, y, z, w]"

|  |  |
|--|--|
|RETURNS: string|A string that looks like "[x, y, z, w]"|

# enum DisplayMode

Specifies a type of display mode StereoKit uses, like
Mixed Reality headset display vs. a PC display, or even just
rendering to an offscreen surface, or not rendering at all!

## DisplayMode.MixedReality

```csharp
static DisplayMode MixedReality
```

Creates an OpenXR instance, and drives display/input
through that.

## DisplayMode.Flatscreen

```csharp
static DisplayMode Flatscreen
```

Creates a flat, Win32 window, and simulates some MR
functionality. Great for debugging.

## DisplayMode.None

```csharp
static DisplayMode None
```

Not tested yet, but this is meant to run StereoKit
without rendering to any display at all. This would allow for
rendering to textures, running a server that can do MR related
tasks, etc.
# enum DepthMode

This is used to determine what kind of depth buffer
StereoKit uses!

## DepthMode.Balanced

```csharp
static DepthMode Balanced
```

Default mode, uses 16 bit on mobile devices like
HoloLens and Quest, and 32 bit on higher powered platforms like
PC. If you need a far view distance even on mobile devices,
prefer D32 or Stencil instead.

## DepthMode.D16

```csharp
static DepthMode D16
```

16 bit depth buffer, this is fast and recommended for
devices like the HoloLens. This is especially important for fast
depth based reprojection. Far view distances will suffer here
though, so keep your clipping far plane as close as possible.

## DepthMode.D32

```csharp
static DepthMode D32
```

32 bit depth buffer, should look great at any distance!
If you must have the best, then this is the best. If you're
interested in this one, Stencil may also be plenty for you, as 24
bit depth is also pretty peachy.

## DepthMode.Stencil

```csharp
static DepthMode Stencil
```

24 bit depth buffer with 8 bits of stencil data. 24 bits
is generally plenty for a depth buffer, so using the rest for
stencil can open up some nice options! StereoKit has limited
stencil support right now though (v0.3).
# enum Display

TODO: remove this in v0.4
This describes the type of display tech used on a Mixed
Reality device. This will be replaced by `DisplayBlend` in v0.4.

## Display.None

```csharp
static Display None
```

Default value, when using this as a search type, it will
fall back to default behavior which defers to platform
preference.

## Display.Opaque

```csharp
static Display Opaque
```

This display is opaque, with no view into the real world!
This is equivalent to a VR headset, or a PC screen.

## Display.Additive

```csharp
static Display Additive
```

This display is transparent, and adds light on top of
the real world. This is equivalent to a HoloLens type of device.

## Display.Blend

```csharp
static Display Blend
```

This is a physically opaque display, but with a camera
passthrough displaying the world behind it anyhow. This would be
like a Varjo XR-1, or phone-camera based AR.

## Display.Passthrough

```csharp
static Display Passthrough
```

Use Display.Blend instead, to be removed in v0.4

## Display.AnyTransparent

```csharp
static Display AnyTransparent
```

This matches either transparent display type! Additive
or Blend. For use when you just want to see the world behind your
application.
# enum DisplayBlend

This describes the way the display's content blends with
whatever is behind it. VR headsets are normally Opaque, but some VR
headsets provide passthrough video, and can support Opaque as well as
Blend, like the Varjo. Transparent AR displays like the HoloLens
would be Additive.

## DisplayBlend.None

```csharp
static DisplayBlend None
```

Default value, when using this as a search type, it will
fall back to default behavior which defers to platform
preference.

## DisplayBlend.Opaque

```csharp
static DisplayBlend Opaque
```

This display is opaque, with no view into the real world!
This is equivalent to a VR headset, or a PC screen.

## DisplayBlend.Additive

```csharp
static DisplayBlend Additive
```

This display is transparent, and adds light on top of
the real world. This is equivalent to a HoloLens type of device.

## DisplayBlend.Blend

```csharp
static DisplayBlend Blend
```

This is a physically opaque display, but with a camera
passthrough displaying the world behind it anyhow. This would be
like a Varjo XR-1, or phone-camera based AR.

## DisplayBlend.AnyTransparent

```csharp
static DisplayBlend AnyTransparent
```

This matches either transparent display type! Additive
or Blend. For use when you just want to see the world behind your
application.
# enum LogLevel

Severity of a log item.

## LogLevel.Diagnostic

```csharp
static LogLevel Diagnostic
```

This is for diagnostic information, where you need to know
details about what -exactly- is going on in the system. This
info doesn't surface by default.

## LogLevel.Info

```csharp
static LogLevel Info
```

This is non-critical information, just to let you know what's
going on.

## LogLevel.Warning

```csharp
static LogLevel Warning
```

Something bad has happened, but it's still within the realm of
what's expected.

## LogLevel.Error

```csharp
static LogLevel Error
```

Danger Will Robinson! Something really bad just happened and
needs fixing!
# enum RenderLayer

When rendering content, you can filter what you're rendering by the
RenderLayer that they're on. This allows you to draw items that are
visible in one render, but not another. For example, you may wish
to draw a player's avatar in a 'mirror' rendertarget, but not in
the primary display. See `Renderer.LayerFilter` for configuring what
the primary display renders.

## RenderLayer.Layer0

```csharp
static RenderLayer Layer0
```

The default render layer. All Draw use this layer unless
otherwise specified.

## RenderLayer.Layer1

```csharp
static RenderLayer Layer1
```

Render layer 1.

## RenderLayer.Layer2

```csharp
static RenderLayer Layer2
```

Render layer 2.

## RenderLayer.Layer3

```csharp
static RenderLayer Layer3
```

Render layer 3.

## RenderLayer.Layer4

```csharp
static RenderLayer Layer4
```

Render layer 4.

## RenderLayer.Layer5

```csharp
static RenderLayer Layer5
```

Render layer 5.

## RenderLayer.Layer6

```csharp
static RenderLayer Layer6
```

Render layer 6.

## RenderLayer.Layer7

```csharp
static RenderLayer Layer7
```

Render layer 7.

## RenderLayer.Layer8

```csharp
static RenderLayer Layer8
```

Render layer 8.

## RenderLayer.Layer9

```csharp
static RenderLayer Layer9
```

Render layer 9.

## RenderLayer.Vfx

```csharp
static RenderLayer Vfx
```

The default VFX layer, StereoKit draws some non-standard
mesh content using this flag, such as lines.

## RenderLayer.All

```csharp
static RenderLayer All
```

This is a flag that specifies all possible layers. If you
want to render all layers, then this is the layer filter
you would use. This is the default for render filtering.

## RenderLayer.AllRegular

```csharp
static RenderLayer AllRegular
```

This is a combination of all layers that are not the VFX layer.
# enum AppFocus

This tells about the app's current focus state, whether it's active and
receiving input, or if it's backgrounded or hidden. This can be
important since apps may still run and render when unfocused, as the app
may still be visible behind the app that _does_ have focus.

## AppFocus.Active

```csharp
static AppFocus Active
```

This StereoKit app is active, focused, and receiving input from the
user. Application should behave as normal.

## AppFocus.Background

```csharp
static AppFocus Background
```

This StereoKit app has been unfocused, something may be compositing
on top of the app such as an OS dashboard. The app is still visible,
but some other thing has focus and is receiving input. You may wish
to pause, disable input tracking, or other such things.

## AppFocus.Hidden

```csharp
static AppFocus Hidden
```

This app is not rendering currently.
# enum AssetState

StereoKit uses an asynchronous loading system to prevent assets from
blocking execution! This means that asset loading systems will return
an asset to you right away, even though it is still being processed
in the background.

## AssetState.ErrorUnsupported

```csharp
static AssetState ErrorUnsupported
```

This asset encountered an issue when parsing the source data. Either
the format is unrecognized by StereoKit, or the data may be corrupt.
Check the logs for additional details.

## AssetState.ErrorNotFound

```csharp
static AssetState ErrorNotFound
```

The asset data was not found! This is most likely an issue with a
bad file path, or file permissions. Check the logs for additional
details.

## AssetState.Error

```csharp
static AssetState Error
```

An unknown error occurred when trying to load the asset! Check the
logs for additional details.

## AssetState.None

```csharp
static AssetState None
```

This asset is in its default state. It has not been told to load
anything, nor does it have any data!

## AssetState.Loading

```csharp
static AssetState Loading
```

This asset is currently queued for loading, but hasn't received any
data yet. Attempting to access metadata or asset data will result in
blocking the app's execution until that data is loaded!

## AssetState.LoadedMeta

```csharp
static AssetState LoadedMeta
```

This asset is still loading, but some of the higher level data is
already available for inspection without blocking the app.
Attempting to access the core asset data will result in blocking the
app's execution until that data is loaded!

## AssetState.Loaded

```csharp
static AssetState Loaded
```

This asset is completely loaded without issues, and is ready for
use!
# enum Memory

For performance sensitive areas, or places dealing with large chunks of
memory, it can be faster to get a reference to that memory rather than
copying it! However, if this isn't explicitly stated, it isn't necessarily
clear what's happening. So this enum allows us to visibly specify what type
of memory reference is occurring.

## Memory.Reference

```csharp
static Memory Reference
```

The chunk of memory involved here is a reference that is still managed or
used by StereoKit! You should _not_ free it, and be extremely cautious
about modifying it.

## Memory.Copy

```csharp
static Memory Copy
```

This memory is now _yours_ and you must free it yourself! Memory has been
allocated, and the data has been copied over to it. Pricey! But safe.
# enum TexType

Textures come in various types and flavors! These are bit-flags
that tell StereoKit what type of texture we want, and how the application
might use it!

## TexType.ImageNomips

```csharp
static TexType ImageNomips
```

A standard color image, without any generated mip-maps.

## TexType.Cubemap

```csharp
static TexType Cubemap
```

A size sided texture that's used for things like skyboxes,
environment maps, and reflection probes. It behaves like a texture
array with 6 textures.

## TexType.Rendertarget

```csharp
static TexType Rendertarget
```

This texture can be rendered to! This is great for textures
that might be passed in as a target to Renderer.Blit, or other
such situations.

## TexType.Depth

```csharp
static TexType Depth
```

This texture contains depth data, not color data!

## TexType.Mips

```csharp
static TexType Mips
```

This texture will generate mip-maps any time the contents
change. Mip-maps are a list of textures that are each half the
size of the one before them! This is used to prevent textures from
'sparkling' or aliasing in the distance.

## TexType.Dynamic

```csharp
static TexType Dynamic
```

This texture's data will be updated frequently from the
CPU (not renders)! This ensures the graphics card stores it
someplace where writes are easy to do quickly.

## TexType.Image

```csharp
static TexType Image
```

A standard color image that also generates mip-maps
automatically.
# enum TexFormat

What type of color information will the texture contain? A
good default here is Rgba32.

## TexFormat.None

```csharp
static TexFormat None
```

A default zero value for TexFormat! Uninitialized formats
will get this value and **** **** up so you know to assign it
properly :)

## TexFormat.Rgba32

```csharp
static TexFormat Rgba32
```

Red/Green/Blue/Transparency data channels, at 8 bits
per-channel in sRGB color space. This is what you'll want most of
the time you're dealing with color images! Matches well with the
Color32 struct! If you're storing normals, rough/metal, or
anything else, use Rgba32Linear.

## TexFormat.Rgba32Linear

```csharp
static TexFormat Rgba32Linear
```

Red/Green/Blue/Transparency data channels, at 8 bits
per-channel in linear color space. This is what you'll want most
of the time you're dealing with color data! Matches well with the
Color32 struct.

## TexFormat.Bgra32

```csharp
static TexFormat Bgra32
```

Red/Green/Blue/Transparency data channels, at 8 bits
per-channel in linear color space. This is what you'll want most
of the time you're dealing with color data! Matches well with the
Color32 struct.

## TexFormat.Bgra32Linear

```csharp
static TexFormat Bgra32Linear
```

Red/Green/Blue/Transparency data channels, at 8 bits
per-channel in linear color space. This is what you'll want most
of the time you're dealing with color data! Matches well with the
Color32 struct.

## TexFormat.Rg11b10

```csharp
static TexFormat Rg11b10
```

Red/Green/Blue/Transparency data channels, at 8 bits
per-channel in linear color space. This is what you'll want most
of the time you're dealing with color data! Matches well with the
Color32 struct.

## TexFormat.Rgb10a2

```csharp
static TexFormat Rgb10a2
```

Red/Green/Blue/Transparency data channels, at 8 bits
per-channel in linear color space. This is what you'll want most
of the time you're dealing with color data! Matches well with the
Color32 struct.

## TexFormat.Rgba64

```csharp
static TexFormat Rgba64
```

TODO: remove during major version update

## TexFormat.Rgba128

```csharp
static TexFormat Rgba128
```

Red/Green/Blue/Transparency data channels at 32 bits
per-channel! Basically 4 floats per color, which is bonkers
expensive. Don't use this unless you know -exactly- what you're
doing.

## TexFormat.R8

```csharp
static TexFormat R8
```

A single channel of data, with 8 bits per-pixel! This
can be great when you're only using one channel, and want to
reduce memory usage. Values in the shader are always 0.0-1.0.

## TexFormat.R16

```csharp
static TexFormat R16
```

A single channel of data, with 16 bits per-pixel! This
is a good format for height maps, since it stores a fair bit of
information in it. Values in the shader are always 0.0-1.0.

## TexFormat.R32

```csharp
static TexFormat R32
```

A single channel of data, with 32 bits per-pixel! This
basically treats each pixel as a generic float, so you can do all
sorts of strange and interesting things with this.

## TexFormat.DepthStencil

```csharp
static TexFormat DepthStencil
```

A depth data format, 24 bits for depth data, and 8 bits
to store stencil information! Stencil data can be used for things
like clipping effects, deferred rendering, or shadow effects.

## TexFormat.Depth32

```csharp
static TexFormat Depth32
```

32 bits of data per depth value! This is pretty detailed,
and is excellent for experiences that have a very far view
distance.

## TexFormat.Depth16

```csharp
static TexFormat Depth16
```

16 bits of depth is not a lot, but it can be enough if
your far clipping plane is pretty close. If you're seeing lots of
flickering where two objects overlap, you either need to bring
your far clip in, or switch to 32/24 bit depth.

## TexFormat.Rgba64u

```csharp
static TexFormat Rgba64u
```

16 bits of depth is not a lot, but it can be enough if
your far clipping plane is pretty close. If you're seeing lots of
flickering where two objects overlap, you either need to bring
your far clip in, or switch to 32/24 bit depth.
# enum TexSample

How does the shader grab pixels from the texture? Or more
specifically, how does the shader grab colors between the provided
pixels? If you'd like an in-depth explanation of these topics, check
out [this exploration of texture filtering](https://medium.com/@bgolus/sharper-mipmapping-using-shader-based-supersampling-ed7aadb47bec)
by graphics wizard Ben Golus.

## TexSample.Linear

```csharp
static TexSample Linear
```

Use a linear blend between adjacent pixels, this creates
a smooth, blurry look when texture resolution is too low.

## TexSample.Point

```csharp
static TexSample Point
```

Choose the nearest pixel's color! This makes your texture
look like pixel art if you're too close.

## TexSample.Anisotropic

```csharp
static TexSample Anisotropic
```

This helps reduce texture blurriness when a surface is
viewed at an extreme angle!
# enum TexAddress

What happens when the shader asks for a texture coordinate
that's outside the texture?? Believe it or not, this happens plenty
often!

## TexAddress.Wrap

```csharp
static TexAddress Wrap
```

Wrap the UV coordinate around to the other side of the
texture! This is basically like a looping texture, and is an
excellent default. If you can see weird bits of color at the edges
of your texture, this may be due to Wrap blending the color with
the other side of the texture, Clamp may be better in such cases.

## TexAddress.Clamp

```csharp
static TexAddress Clamp
```

Clamp the UV coordinates to the edge of the texture!
This'll create color streaks that continue to forever. This is
actually really great for non-looping textures that you know will
always be accessed on the 0-1 range.

## TexAddress.Mirror

```csharp
static TexAddress Mirror
```

Like Wrap, but it reflects the image each time! Who needs
this? I'm not sure!! But the graphics card can do it, so now you
can too!
# enum Transparency

Also known as 'alpha' for those in the know. But there's
actually more than one type of transparency in rendering! The
horrors. We're keepin' it fairly simple for now, so you get three
options!

## Transparency.None

```csharp
static Transparency None
```

Not actually transparent! This is opaque! Solid! It's
the default option, and it's the fastest option! Opaque objects
write to the z-buffer, the occlude pixels behind them, and they
can be used as input to important Mixed Reality features like
Late Stage Reprojection that'll make your view more stable!

## Transparency.Blend

```csharp
static Transparency Blend
```

This will blend with the pixels behind it. This is
transparent! You may not want to write to the z-buffer, and it's
slower than opaque materials.

## Transparency.Add

```csharp
static Transparency Add
```

This will straight up add the pixel color to the color
buffer! This usually looks -really- glowy, so it makes for good
particles or lighting effects.
# enum Cull

Culling is discarding an object from the render pipeline!
This enum describes how mesh faces get discarded on the graphics
card. With culling set to none, you can double the number of pixels
the GPU ends up drawing, which can have a big impact on performance.
None can be appropriate in cases where the mesh is designed to be
'double sided'. Front can also be helpful when you want to flip a
mesh 'inside-out'!

## Cull.Back

```csharp
static Cull Back
```

Discard if the back of the triangle face is pointing
towards the camera. This is the default behavior.

## Cull.Front

```csharp
static Cull Front
```

Discard if the front of the triangle face is pointing
towards the camera. This is opposite the default behavior.

## Cull.None

```csharp
static Cull None
```

No culling at all! Draw the triangle regardless of which
way it's pointing.
# enum DepthTest

Depth test describes how this material looks at and responds
to depth information in the zbuffer! The default is Less, which means
if the material pixel's depth is Less than the existing depth data,
(basically, is this in front of some other object) it will draw that
pixel. Similarly, Greater would only draw the material if it's
'behind' the depth buffer. Always would just draw all the time, and
not read from the depth buffer at all.

## DepthTest.Less

```csharp
static DepthTest Less
```

Default behavior, pixels behind the depth buffer will be
discarded, and pixels in front of it will be drawn.

## DepthTest.LessOrEq

```csharp
static DepthTest LessOrEq
```

Pixels behind the depth buffer will be discarded, and
pixels in front of, or at the depth buffer's value it will be
drawn. This could be great for things that might be sitting
exactly on a floor or wall.

## DepthTest.Greater

```csharp
static DepthTest Greater
```

Pixels in front of the zbuffer will be discarded! This
is opposite of how things normally work. Great for drawing
indicators that something is occluded by a wall or other
geometry.

## DepthTest.GreaterOrEq

```csharp
static DepthTest GreaterOrEq
```

Pixels in front of (or exactly at) the zbuffer will be
discarded! This is opposite of how things normally work. Great
for drawing indicators that something is occluded by a wall or
other geometry.

## DepthTest.Equal

```csharp
static DepthTest Equal
```

Only draw pixels if they're at exactly the same depth as
the zbuffer!

## DepthTest.NotEqual

```csharp
static DepthTest NotEqual
```

Draw any pixel that's not exactly at the value in the
zbuffer.

## DepthTest.Always

```csharp
static DepthTest Always
```

Don't look at the zbuffer at all, just draw everything,
always, all the time! At this point, the order at which the mesh
gets drawn will be  super important, so don't forget about
`Material.QueueOffset`!

## DepthTest.Never

```csharp
static DepthTest Never
```

Never draw a pixel, regardless of what's in the zbuffer.
I can think of better ways to do this, but uhh, this is here for
completeness! Maybe you can find a use for it.
# enum MaterialParam

TODO: v0.4 This may need significant revision?
What type of data does this material parameter need? This is
used to tell the shader how large the data is, and where to attach it
to on the shader.

## MaterialParam.Unknown

```csharp
static MaterialParam Unknown
```

This data type is not currently recognized. Please
report your case on GitHub Issues!

## MaterialParam.Float

```csharp
static MaterialParam Float
```

A single 32 bit float value.

## MaterialParam.Color128

```csharp
static MaterialParam Color128
```

A color value described by 4 floating point values. Memory-wise this is
the same as a Vector4, but in the shader this variable has a ':color'
tag applied to it using StereoKits's shader info syntax, indicating it's
a color value. Color values for shaders should be in linear space, not
gamma.

## MaterialParam.Vector2

```csharp
static MaterialParam Vector2
```

A 2 component vector composed of floating point values.

## MaterialParam.Vector3

```csharp
static MaterialParam Vector3
```

A 3 component vector composed of floating point values.

## MaterialParam.Vector4

```csharp
static MaterialParam Vector4
```

A 4 component vector composed of floating point values.

## MaterialParam.Vector

```csharp
static MaterialParam Vector
```

A 4 component vector composed of floating point values.
TODO: Remove in v0.4

## MaterialParam.Matrix

```csharp
static MaterialParam Matrix
```

A 4x4 matrix of floats.

## MaterialParam.Texture

```csharp
static MaterialParam Texture
```

Texture information!

## MaterialParam.Int

```csharp
static MaterialParam Int
```

A 1 component vector composed of signed integers.

## MaterialParam.Int2

```csharp
static MaterialParam Int2
```

A 2 component vector composed of signed integers.

## MaterialParam.Int3

```csharp
static MaterialParam Int3
```

A 3 component vector composed of signed integers.

## MaterialParam.Int4

```csharp
static MaterialParam Int4
```

A 4 component vector composed of signed integers.

## MaterialParam.UInt

```csharp
static MaterialParam UInt
```

A 1 component vector composed of unsigned integers. This may also be a
boolean.

## MaterialParam.UInt2

```csharp
static MaterialParam UInt2
```

A 2 component vector composed of unsigned integers.

## MaterialParam.UInt3

```csharp
static MaterialParam UInt3
```

A 3 component vector composed of unsigned integers.

## MaterialParam.UInt4

```csharp
static MaterialParam UInt4
```

A 4 component vector composed of unsigned integers.
# enum TextFit

This enum describes how text layout behaves within the space
it is given.

## TextFit.Wrap

```csharp
static TextFit Wrap
```

The text will wrap around to the next line down when it
reaches the end of the space on the X axis.

## TextFit.Clip

```csharp
static TextFit Clip
```

When the text reaches the end, it is simply truncated
and no longer visible.

## TextFit.Squeeze

```csharp
static TextFit Squeeze
```

If the text is too large to fit in the space provided,
it will be scaled down to fit inside. This will not scale up.

## TextFit.Exact

```csharp
static TextFit Exact
```

If the text is larger, or smaller than the space
provided, it will scale down or up to fill the space.

## TextFit.Overflow

```csharp
static TextFit Overflow
```

The text will ignore the containing space, and just keep
on going.
# enum TextAlign

A bit-flag enum for describing alignment or positioning.
Items can be combined using the '|' operator, like so:

## TextAlign.XLeft

```csharp
static TextAlign XLeft
```

On the x axis, this item should start on the left.

## TextAlign.YTop

```csharp
static TextAlign YTop
```

On the y axis, this item should start at the top.

## TextAlign.XCenter

```csharp
static TextAlign XCenter
```

On the x axis, the item should be centered.

## TextAlign.YCenter

```csharp
static TextAlign YCenter
```

On the y axis, the item should be centered.

## TextAlign.XRight

```csharp
static TextAlign XRight
```

On the x axis, this item should start on the right.

## TextAlign.YBottom

```csharp
static TextAlign YBottom
```

On the y axis, this item should start on the bottom.

## TextAlign.Center

```csharp
static TextAlign Center
```

Center on both X and Y axes. This is a combination of
XCenter and YCenter.

## TextAlign.CenterLeft

```csharp
static TextAlign CenterLeft
```

Start on the left of the X axis, center on the Y axis.
This is a combination of XLeft and YCenter.

## TextAlign.CenterRight

```csharp
static TextAlign CenterRight
```

Start on the right of the X axis, center on the Y axis.
This is a combination of XRight and YCenter.

## TextAlign.TopCenter

```csharp
static TextAlign TopCenter
```

Center on the X axis, and top on the Y axis. This is a
combination of XCenter and YTop.

## TextAlign.TopLeft

```csharp
static TextAlign TopLeft
```

Start on the left of the X axis, and top on the Y axis.
This is a combination of XLeft and YTop.

## TextAlign.TopRight

```csharp
static TextAlign TopRight
```

Start on the right of the X axis, and top on the Y axis.
This is a combination of XRight and YTop.

## TextAlign.BottomCenter

```csharp
static TextAlign BottomCenter
```

Center on the X axis, and bottom on the Y axis. This is
a combination of XCenter and YBottom.

## TextAlign.BottomLeft

```csharp
static TextAlign BottomLeft
```

Start on the left of the X axis, and bottom on the Y
axis. This is a combination of XLeft and YBottom.

## TextAlign.BottomRight

```csharp
static TextAlign BottomRight
```

Start on the right of the X axis, and bottom on the Y
axis.This is a combination of XRight and YBottom.
# enum SolidType

This describes the behavior of a 'Solid' physics object! The
physics engine will apply forces differently based on this type.

## SolidType.Normal

```csharp
static SolidType Normal
```

This object behaves like a normal physical object, it'll
fall, get pushed around, and generally be susceptible to physical
forces! This is a 'Dynamic' body in physics simulation terms.

## SolidType.Immovable

```csharp
static SolidType Immovable
```

Immovable objects are always stationary! They have
infinite mass, zero velocity, and can't collide with Immovable of
Unaffected types.

## SolidType.Unaffected

```csharp
static SolidType Unaffected
```

Unaffected objects have infinite mass, but can have a
velocity! They'll move under their own forces, but nothing in the
simulation will affect them. They don't collide with Immovable or
Unaffected types.
# enum AnimMode

Describes how an animation is played back, and what to do when
the animation hits the end.

## AnimMode.Loop

```csharp
static AnimMode Loop
```

If the animation reaches the end, it will always loop
back around to the start again.

## AnimMode.Once

```csharp
static AnimMode Once
```

When the animation reaches the end, it will freeze
in-place.

## AnimMode.Manual

```csharp
static AnimMode Manual
```

The animation will not progress on its own, and instead
must be driven by providing information to the model's AnimTime
or AnimCompletion properties.
# enum SpriteType

The way the Sprite is stored on the backend! Does it get
batched and atlased for draw efficiency, or is it a single image?

## SpriteType.Atlased

```csharp
static SpriteType Atlased
```

The sprite will be batched onto an atlas texture so all
sprites can be drawn in a single pass. This is excellent for
performance! The only thing to watch out for here, adding a sprite
to an atlas will rebuild the atlas texture! This can be a bit
expensive, so it's recommended to add all sprites to an atlas at
start, rather than during runtime. Also, if an image is too large,
it may take up too much space on the atlas, and may be better as a
Single sprite type.

## SpriteType.Single

```csharp
static SpriteType Single
```

This sprite is on its own texture. This is best for large
images, items that get loaded and unloaded during runtime, or for
sprites that may have edge artifacts or severe 'bleed' from
adjacent atlased images.
# enum RenderClear

When rendering to a rendertarget, this tells if and what of the
rendertarget gets cleared before rendering. For example, if you
are assembling a sheet of images, you may want to clear
everything on the first image draw, but not clear on subsequent
draws.

## RenderClear.None

```csharp
static RenderClear None
```

Don't clear anything, leave it as it is.

## RenderClear.Color

```csharp
static RenderClear Color
```

Clear the rendertarget's color data.

## RenderClear.Depth

```csharp
static RenderClear Depth
```

Clear the rendertarget's depth data, if present.

## RenderClear.All

```csharp
static RenderClear All
```

Clear both color and depth data.
# enum Projection

The projection mode used by StereoKit for the main camera! You
can use this with Renderer.Projection. These options are only
available in flatscreen mode, as MR headsets provide very
specific projection matrices.

## Projection.Perspective

```csharp
static Projection Perspective
```

This is the default projection mode, and the one you're most likely
to be familiar with! This is where parallel lines will converge as
they go into the distance.

## Projection.Ortho

```csharp
static Projection Ortho
```

Orthographic projection mode is often used for tools, 2D rendering,
thumbnails of 3D objects, or other similar cases. In this mode,
parallel lines remain parallel regardless of how far they travel.
# enum PickerMode

When opening the Platform.FilePicker, this enum describes
how the picker should look and behave.

## PickerMode.Open

```csharp
static PickerMode Open
```

Allow opening a single file.

## PickerMode.Save

```csharp
static PickerMode Save
```

Allow the user to enter or select the name of the
destination file.
# enum TextContext

Soft keyboard layouts are often specific to the type of text that they're
editing! This enum is a collection of common text contexts that SK can pass
along to the OS's soft keyboard for a more optimal layout.

## TextContext.Text

```csharp
static TextContext Text
```

General text editing, this is the most common type of text, and would
result in a 'standard' keyboard layout.

## TextContext.Number

```csharp
static TextContext Number
```

Numbers and numerical values.

## TextContext.Uri

```csharp
static TextContext Uri
```

This text specifically represents some kind of URL/URI address.

## TextContext.Password

```csharp
static TextContext Password
```

This is a password, and should not be visible when typed!
# enum InputSource

What type of device is the source of the pointer? This is a
bit-flag that can contain some input source family information.

## InputSource.Any

```csharp
static InputSource Any
```

Matches with all input sources!

## InputSource.Hand

```csharp
static InputSource Hand
```

Matches with any hand input source.

## InputSource.HandLeft

```csharp
static InputSource HandLeft
```

Matches with left hand input sources.

## InputSource.HandRight

```csharp
static InputSource HandRight
```

Matches with right hand input sources.

## InputSource.Gaze

```csharp
static InputSource Gaze
```

Matches with Gaze category input sources.

## InputSource.GazeHead

```csharp
static InputSource GazeHead
```

Matches with the head gaze input source.

## InputSource.GazeEyes

```csharp
static InputSource GazeEyes
```

Matches with the eye gaze input source.

## InputSource.GazeCursor

```csharp
static InputSource GazeCursor
```

Matches with mouse cursor simulated gaze as an input source.

## InputSource.CanPress

```csharp
static InputSource CanPress
```

Matches with any input source that has an activation button!
# enum Handed

An enum for indicating which hand to use!

## Handed.Left

```csharp
static Handed Left
```

Left hand.

## Handed.Right

```csharp
static Handed Right
```

Right hand.

## Handed.Max

```csharp
static Handed Max
```

The number of hands one generally has, this is much nicer
than doing a for loop with '2' as the condition! It's much clearer
when you can loop Hand.Max times instead.
# enum BtnState

A bit-flag for the current state of a button input.

## BtnState.Inactive

```csharp
static BtnState Inactive
```

Is the button currently up, unpressed?

## BtnState.Active

```csharp
static BtnState Active
```

Is the button currently down, pressed?

## BtnState.JustInactive

```csharp
static BtnState JustInactive
```

Has the button just been released? Only true for a single frame.

## BtnState.JustActive

```csharp
static BtnState JustActive
```

Has the button just been pressed? Only true for a single frame.

## BtnState.Changed

```csharp
static BtnState Changed
```

Has the button just changed state this frame?

## BtnState.Any

```csharp
static BtnState Any
```

Matches with all states!
# enum TrackState

This is the tracking state of a sensory input in the world,
like a controller's position sensor, or a QR code identified by a
tracking system.

## TrackState.Lost

```csharp
static TrackState Lost
```

The system has no current knowledge about the state of
this input. It may be out of visibility, or possibly just
disconnected.

## TrackState.Inferred

```csharp
static TrackState Inferred
```

The system doesn't know for sure where this is, but it
has an educated guess that may be inferred from previous data at
a lower quality. For example, a controller may still have
accelerometer data after going out of view, which can still be
accurate for a short time after losing optical tracking.

## TrackState.Known

```csharp
static TrackState Known
```

The system actively knows where this input is. Within
the constraints of the relevant hardware's capabilities, this is
as accurate as it gets!
# enum Key

A collection of system key codes, representing keyboard
characters and mouse buttons. Based on VK codes.

## Key.None

```csharp
static Key None
```

Doesn't represent a key, generally means this item has not been set to
any particular value!

## Key.MouseLeft

```csharp
static Key MouseLeft
```

Left mouse button.

## Key.MouseRight

```csharp
static Key MouseRight
```

Right mouse button.

## Key.MouseCenter

```csharp
static Key MouseCenter
```

Center mouse button.

## Key.MouseForward

```csharp
static Key MouseForward
```

Mouse forward button.

## Key.MouseBack

```csharp
static Key MouseBack
```

Mouse back button.

## Key.Backspace

```csharp
static Key Backspace
```

Backspace

## Key.Tab

```csharp
static Key Tab
```

Tab

## Key.Return

```csharp
static Key Return
```

Return, or Enter.

## Key.Shift

```csharp
static Key Shift
```

Left or right Shift.

## Key.Ctrl

```csharp
static Key Ctrl
```

Left or right Control key.

## Key.Alt

```csharp
static Key Alt
```

Left or right Alt key.

## Key.CapsLock

```csharp
static Key CapsLock
```

This behaves a little differently! This tells the toggle
state of caps lock, rather than the key state itself.

## Key.Esc

```csharp
static Key Esc
```

Escape

## Key.Space

```csharp
static Key Space
```

Space

## Key.End

```csharp
static Key End
```

End

## Key.Home

```csharp
static Key Home
```

Home

## Key.Left

```csharp
static Key Left
```

Left arrow key.

## Key.Right

```csharp
static Key Right
```

Right arrow key.

## Key.Up

```csharp
static Key Up
```

Up arrow key.

## Key.Down

```csharp
static Key Down
```

Down arrow key.

## Key.PageUp

```csharp
static Key PageUp
```

Page up

## Key.PageDown

```csharp
static Key PageDown
```

Page down

## Key.Printscreen

```csharp
static Key Printscreen
```

Printscreen

## Key.Insert

```csharp
static Key Insert
```

Any Insert key.

## Key.Del

```csharp
static Key Del
```

Any Delete key.

## Key.N0

```csharp
static Key N0
```

Keyboard top row 0, with shift is ')'.

## Key.N1

```csharp
static Key N1
```

Keyboard top row 1, with shift is '!'.

## Key.N2

```csharp
static Key N2
```

Keyboard top row 2, with shift is '@'.

## Key.N3

```csharp
static Key N3
```

Keyboard top row 3, with shift is '#'.

## Key.N4

```csharp
static Key N4
```

Keyboard top row 4, with shift is '$'.

## Key.N5

```csharp
static Key N5
```

Keyboard top row 5, with shift is '%'.

## Key.N6

```csharp
static Key N6
```

Keyboard top row 6, with shift is '^'.

## Key.N7

```csharp
static Key N7
```

Keyboard top row 7, with shift is '&'.

## Key.N8

```csharp
static Key N8
```

Keyboard top row 8, with shift is '*'.

## Key.N9

```csharp
static Key N9
```

Keyboard top row 9, with shift is '('.

## Key.A

```csharp
static Key A
```

a/A

## Key.B

```csharp
static Key B
```

b/B

## Key.C

```csharp
static Key C
```

c/C

## Key.D

```csharp
static Key D
```

d/D

## Key.E

```csharp
static Key E
```

e/E

## Key.F

```csharp
static Key F
```

f/F

## Key.G

```csharp
static Key G
```

g/G

## Key.H

```csharp
static Key H
```

h/H

## Key.I

```csharp
static Key I
```

i/I

## Key.J

```csharp
static Key J
```

j/J

## Key.K

```csharp
static Key K
```

k/K

## Key.L

```csharp
static Key L
```

l/L

## Key.M

```csharp
static Key M
```

m/M

## Key.N

```csharp
static Key N
```

n/N

## Key.O

```csharp
static Key O
```

o/O

## Key.P

```csharp
static Key P
```

p/P

## Key.Q

```csharp
static Key Q
```

q/Q

## Key.R

```csharp
static Key R
```

r/R

## Key.S

```csharp
static Key S
```

s/S

## Key.T

```csharp
static Key T
```

t/T

## Key.U

```csharp
static Key U
```

u/U

## Key.V

```csharp
static Key V
```

v/V

## Key.W

```csharp
static Key W
```

w/W

## Key.X

```csharp
static Key X
```

x/X

## Key.Y

```csharp
static Key Y
```

y/Y

## Key.Z

```csharp
static Key Z
```

z/Z

## Key.Num0

```csharp
static Key Num0
```

0 on the numpad, when numlock is on.

## Key.Num1

```csharp
static Key Num1
```

1 on the numpad, when numlock is on.

## Key.Num2

```csharp
static Key Num2
```

2 on the numpad, when numlock is on.

## Key.Num3

```csharp
static Key Num3
```

3 on the numpad, when numlock is on.

## Key.Num4

```csharp
static Key Num4
```

4 on the numpad, when numlock is on.

## Key.Num5

```csharp
static Key Num5
```

5 on the numpad, when numlock is on.

## Key.Num6

```csharp
static Key Num6
```

6 on the numpad, when numlock is on.

## Key.Num7

```csharp
static Key Num7
```

7 on the numpad, when numlock is on.

## Key.Num8

```csharp
static Key Num8
```

8 on the numpad, when numlock is on.

## Key.Num9

```csharp
static Key Num9
```

9 on the numpad, when numlock is on.

## Key.F1

```csharp
static Key F1
```

Function key F1.

## Key.F2

```csharp
static Key F2
```

Function key F2.

## Key.F3

```csharp
static Key F3
```

Function key F3.

## Key.F4

```csharp
static Key F4
```

Function key F4.

## Key.F5

```csharp
static Key F5
```

Function key F5.

## Key.F6

```csharp
static Key F6
```

Function key F6.

## Key.F7

```csharp
static Key F7
```

Function key F7.

## Key.F8

```csharp
static Key F8
```

Function key F8.

## Key.F9

```csharp
static Key F9
```

Function key F9.

## Key.F10

```csharp
static Key F10
```

Function key F10.

## Key.F11

```csharp
static Key F11
```

Function key F11.

## Key.F12

```csharp
static Key F12
```

Function key F12.

## Key.Comma

```csharp
static Key Comma
```

,/<

## Key.Period

```csharp
static Key Period
```

./>

## Key.SlashFwd

```csharp
static Key SlashFwd
```

/

## Key.SlashBack

```csharp
static Key SlashBack
```

\

## Key.Semicolon

```csharp
static Key Semicolon
```

;/:

## Key.Apostrophe

```csharp
static Key Apostrophe
```

'/"

## Key.BracketOpen

```csharp
static Key BracketOpen
```

[/{

## Key.BracketClose

```csharp
static Key BracketClose
```

]/}

## Key.Minus

```csharp
static Key Minus
```

-/_

## Key.Equals

```csharp
static Key Equals
```

=/+

## Key.Backtick

```csharp
static Key Backtick
```

`/~

## Key.LCmd

```csharp
static Key LCmd
```

The Windows/Mac Command button on the left side of the keyboard.

## Key.RCmd

```csharp
static Key RCmd
```

The Windows/Mac Command button on the right side of the keyboard.

## Key.Multiply

```csharp
static Key Multiply
```

Numpad '*', NOT the same as number row '*'.

## Key.Add

```csharp
static Key Add
```

Numpad '+', NOT the same as number row '+'.

## Key.Subtract

```csharp
static Key Subtract
```

Numpad '-', NOT the same as number row '-'.

## Key.Decimal

```csharp
static Key Decimal
```

Numpad '.', NOT the same as character '.'.

## Key.Divide

```csharp
static Key Divide
```

Numpad '/', NOT the same as character '/'.

## Key.MAX

```csharp
static Key MAX
```

Maximum value for key codes.
# enum WorldRefresh

A settings flag that lets you describe the behavior of how
StereoKit will refresh data about the world mesh, if applicable. This
is used with `World.RefreshType`.

## WorldRefresh.Area

```csharp
static WorldRefresh Area
```

Refreshing occurs when the user leaves the area that was
most recently scanned. This area is a sphere that is 0.5 of the
World.RefreshRadius.

## WorldRefresh.Timer

```csharp
static WorldRefresh Timer
```

Refreshing happens at timer intervals. If an update
doesn't happen in time, the next update will happen as soon as
possible. The timer interval is configurable via
`World.RefreshInterval`.
# enum BackendXRType

This describes what technology is being used to power StereoKit's
XR backend.

## BackendXRType.None

```csharp
static BackendXRType None
```

StereoKit is not using an XR backend of any sort. That means
the application is flatscreen and has the simulator disabled.

## BackendXRType.Simulator

```csharp
static BackendXRType Simulator
```

StereoKit is using the flatscreen XR simulator. Inputs are
emulated, and some advanced XR functionality may not be
available.

## BackendXRType.OpenXR

```csharp
static BackendXRType OpenXR
```

StereoKit is currently powered by OpenXR! This means we're
running on a real XR device. Not all OpenXR runtimes provide
the same functionality, but we will have access to more fun
stuff :)

## BackendXRType.WebXR

```csharp
static BackendXRType WebXR
```

StereoKit is running in a browser, and is using WebXR!
# enum BackendPlatform

This describes the platform that StereoKit is running on.

## BackendPlatform.Win32

```csharp
static BackendPlatform Win32
```

This is running as a Windows app using the Win32 APIs.

## BackendPlatform.Uwp

```csharp
static BackendPlatform Uwp
```

This is running as a Windows app using the UWP APIs.

## BackendPlatform.Linux

```csharp
static BackendPlatform Linux
```

This is running as a Linux app.

## BackendPlatform.Android

```csharp
static BackendPlatform Android
```

This is running as an Android app.

## BackendPlatform.Web

```csharp
static BackendPlatform Web
```

This is running in a browser.
# enum BackendGraphics

This describes the graphics API thatStereoKit is using for rendering.

## BackendGraphics.None

```csharp
static BackendGraphics None
```

An invalid default value. Right now, this may likely indicate a variety
of OpenGL.

## BackendGraphics.D3D11

```csharp
static BackendGraphics D3D11
```

DirectX's Direct3D11 is used for rendering!
# enum LogColors

The log tool will write to the console with annotations for console
colors, which helps with readability, but isn't always supported.
These are the options available for configuring those colors.

## LogColors.Ansi

```csharp
static LogColors Ansi
```

Use console coloring annotations.

## LogColors.None

```csharp
static LogColors None
```

Scrape out any color annotations, so logs are all completely
plain text.
# struct SKSettings

StereoKit initialization settings! Setup SK.settings with
your data before calling SK.Initialize.

## SKSettings.displayPreference

```csharp
DisplayMode displayPreference
```

Which display type should we try to load? Default is
`DisplayMode.MixedReality`.

## SKSettings.blendPreference

```csharp
DisplayBlend blendPreference
```

What type of background blend mode do we prefer for this
application? Are you trying to build an Opaque/Immersive/VR app,
or would you like the display to be AnyTransparent, so the world
will show up behind your content, if that's an option? Note that
this is a preference only, and if it's not available on this
device, the app will fall back to the runtime's preference
instead! By default, (DisplayBlend.None) this uses the runtime's
preference.

## SKSettings.noFlatscreenFallback

```csharp
bool noFlatscreenFallback{ get set }
```

If the preferred display fails, should we avoid falling
back to flatscreen and just crash out? Default is false.

## SKSettings.depthMode

```csharp
DepthMode depthMode
```

What kind of depth buffer should StereoKit use? A fast
one, a detailed one, one that uses stencils? By default,
StereoKit uses a balanced mix depending on platform, prioritizing
speed but opening up when there's headroom.

## SKSettings.logFilter

```csharp
LogLevel logFilter
```

The default log filtering level. This can be changed at
runtime, but this allows you to set the log filter before
Initialization occurs, so you can choose to get information from
that. Default is LogLevel.Info.

## SKSettings.overlayApp

```csharp
bool overlayApp{ get set }
```

If the runtime supports it, should this application run
as an overlay above existing applications? Check
SK.System.overlayApp after initialization to see if the runtime
could comply with this flag. This will always force StereoKit to
work in a blend compositing mode.

## SKSettings.overlayPriority

```csharp
uint overlayPriority
```

For overlay applications, this is the order in which
apps should be composited together. 0 means first, bottom of the
stack, and uint.MaxValue is last, on top of the stack.

## SKSettings.flatscreenPosX

```csharp
int flatscreenPosX
```

If using Runtime.Flatscreen, the pixel position of the
window on the screen.

## SKSettings.flatscreenPosY

```csharp
int flatscreenPosY
```

If using Runtime.Flatscreen, the pixel position of the
window on the screen.

## SKSettings.flatscreenWidth

```csharp
int flatscreenWidth
```

If using Runtime.Flatscreen, the pixel size of the
window on the screen.

## SKSettings.flatscreenHeight

```csharp
int flatscreenHeight
```

If using Runtime.Flatscreen, the pixel size of the
window on the screen.

## SKSettings.disableFlatscreenMRSim

```csharp
bool disableFlatscreenMRSim{ get set }
```

By default, StereoKit will simulate Mixed Reality input
so developers can test MR spaces without being in a headset. If
You don't want this, you can disable it with this setting!

## SKSettings.disableUnfocusedSleep

```csharp
bool disableUnfocusedSleep{ get set }
```

By default, StereoKit will slow down when the
application is out of focus. This is useful for saving processing
power while the app is out-of-focus, but may not always be
desired. In particular, running multiple copies of a SK app for
testing networking code may benefit from this setting.

## SKSettings.appName

```csharp
string appName{ get set }
```

Name of the application, this shows up an the top of the
Win32 window, and is submitted to OpenXR. OpenXR caps this at 128
characters.

## SKSettings.assetsFolder

```csharp
string assetsFolder{ get set }
```

Where to look for assets when loading files! Final path
will look like '[assetsFolder]/[file]', so a trailing '/' is
unnecessary.
# struct SystemInfo

Information about a system's capabilities and properties!

## SystemInfo.displayType

```csharp
Display displayType
```

The type of display this device has.

## SystemInfo.displayWidth

```csharp
int displayWidth
```

Width of the display surface, in pixels! For a stereo
display, this will be the width of a single eye.

## SystemInfo.displayHeight

```csharp
int displayHeight
```

Height of the display surface, in pixels! For a stereo
display, this will be the height of a single eye.

## SystemInfo.spatialBridgePresent

```csharp
bool spatialBridgePresent{ get }
```

Does the device we're currently on have the spatial
graph bridge extension? The extension is provided through the
function `World.FromSpatialNode`. This allows OpenXR to talk with
certain windows APIs, such as the QR code API that provides Graph
Node GUIDs for the pose.

## SystemInfo.perceptionBridgePresent

```csharp
bool perceptionBridgePresent{ get }
```

Can the device work with externally provided spatial
anchors, like UWP's `Windows.Perception.Spatial.SpatialAnchor`

## SystemInfo.eyeTrackingPresent

```csharp
bool eyeTrackingPresent{ get }
```

Does the device we're on have eye tracking support
present? This is _not_ an indicator that the user has given the
application permission to access this information. See
`Input.Gaze` for how to use this data.

## SystemInfo.overlayApp

```csharp
bool overlayApp{ get }
```

This tells if the app was successfully started as an
overlay application. If this is true, then expect this
application to be composited with other content below it!

## SystemInfo.worldOcclusionPresent

```csharp
bool worldOcclusionPresent{ get }
```

Does this device support world occlusion of digital
objects? If this is true, then World.OcclusionEnabled can be set
to true, and World.OcclusionMaterial can be modified.

## SystemInfo.worldRaycastPresent

```csharp
bool worldRaycastPresent{ get }
```

Can this device get ray intersections from the
environment? If this is true, then World.RaycastEnabled can be
set to true, and World.Raycast can be used.
# struct UISettings

Visual properties and spacing of the UI system.

## UISettings.padding

```csharp
float padding
```

Spacing between an item and its parent, in meters.

## UISettings.gutter

```csharp
float gutter
```

Spacing between sibling items, in meters.

## UISettings.depth

```csharp
float depth
```

The Z depth of 3D UI elements, in meters.

## UISettings.backplateDepth

```csharp
float backplateDepth
```

How far up does the white back-border go on UI elements?
This is a 0-1 percentage of the depth value.

## UISettings.backplateBorder

```csharp
float backplateBorder
```

How wide is the back-border around the UI elements? In
meters.
# struct Vertex

This represents a single vertex in a Mesh, all StereoKit
Meshes currently use this exact layout!

It's good to fill out all values of a Vertex explicitly, as default
values for the normal (0,0,0) and color (0,0,0,0) will cause your
mesh to appear completely black, or even transparent in most shaders!

## Vertex.pos

```csharp
Vec3 pos
```

Position of the vertex, in model space coordinates.

## Vertex.norm

```csharp
Vec3 norm
```

The normal of this vertex, or the direction the vertex is
facing. Preferably normalized.

## Vertex.uv

```csharp
Vec2 uv
```

The texture coordinates at this vertex.

## Vertex.col

```csharp
Color32 col
```

The color of the vertex. If you aren't using it, set it to
white.

## Vertex.Vertex
```csharp
void Vertex(Vec3 position, Vec3 normal)
```
Create a new Vertex, use the overloads to take advantage
of default values. Vertex color defaults to White. UV defaults to
(0,0).

|  |  |
|--|--|
|Vec3 position|Location of the Vertex, this is typically             meters in Model space.|
|Vec3 normal|The direction the Vertex is facing. Never             leave this as zero, or your lighting may turn out black! A good             default value if you _don't_ know what to put here is (0,1,0),             but a Mesh composed entirely of this value will have flat             lighting.|
```csharp
void Vertex(Vec3 position, Vec3 normal, Vec2 textureCoordinates)
```
Create a new Vertex, use the overloads to take advantage
of default values. Vertex color defaults to White.

|  |  |
|--|--|
|Vec3 position|Location of the Vertex, this is typically             meters in Model space.|
|Vec3 normal|The direction the Vertex is facing. Never             leave this as zero, or your lighting may turn out black! A good             default value if you _don't_ know what to put here is (0,1,0),             but a Mesh composed entirely of this value will have flat             lighting.|
|Vec2 textureCoordinates|What part of a texture is this             Vertex anchored to? (0,0) is top left of the texture, and (1,1)             is the bottom right.|
```csharp
void Vertex(Vec3 position, Vec3 normal, Vec2 textureCoordinates, Color32 color)
```
Create a new Vertex, use the overloads to take advantage
of default values.

|  |  |
|--|--|
|Vec3 position|Location of the Vertex, this is typically             meters in Model space.|
|Vec3 normal|The direction the Vertex is facing. Never             leave this as zero, or your lighting may turn out black! A good             default value if you _don't_ know what to put here is (0,1,0),             but a Mesh composed entirely of this value will have flat             lighting.|
|Vec2 textureCoordinates|What part of a texture is this             Vertex anchored to? (0,0) is top left of the texture, and (1,1)             is the bottom right.|
|Color32 color|The color of the Vertex, StereoKit's default             shaders treat this as a multiplicative modifier for the             Material's albedo/diffuse color, but different shaders sometimes             treat this value differently. A good default here is white, black             will cause your model to turn out completely black.|

# struct LinePoint

Used to represent lines for the line drawing functions! This is just a snapshot of
information about each individual point on a line.

## LinePoint.pt

```csharp
Vec3 pt
```

Location of the line point

## LinePoint.thickness

```csharp
float thickness
```

Total thickness of the line, in meters.

## LinePoint.color

```csharp
Color32 color
```

The vertex color for the line at this position.
# static class BtnStateExtensions

A collection of extension methods for the BtnState enum that makes
bit-field checks a little easier.

## BtnStateExtensions.IsActive
```csharp
static bool IsActive(BtnState state)
```
Is the button pressed?

|  |  |
|--|--|
|RETURNS: bool|True if pressed, false if not.|


## BtnStateExtensions.IsJustActive
```csharp
static bool IsJustActive(BtnState state)
```
Has the button just been pressed this frame?

|  |  |
|--|--|
|RETURNS: bool|True if pressed, false if not.|


## BtnStateExtensions.IsJustInactive
```csharp
static bool IsJustInactive(BtnState state)
```
Has the button just been released this frame?

|  |  |
|--|--|
|RETURNS: bool|True if released, false if not.|


## BtnStateExtensions.IsChanged
```csharp
static bool IsChanged(BtnState state)
```
Was the button either presses or released this frame?

|  |  |
|--|--|
|RETURNS: bool|True if the state just changed this frame, false if not.|

# struct Pointer

Pointer is an abstraction of a number of different input
sources, and a way to surface input events!

## Pointer.source

```csharp
InputSource source
```

What input source did this pointer come from? This is
a bit-flag that contains input family and capability
information.

## Pointer.tracked

```csharp
BtnState tracked
```

Is the pointer source being tracked right now?

## Pointer.state

```csharp
BtnState state
```

What is the state of the input source's 'button', if it
has one?

## Pointer.ray

```csharp
Ray ray
```

A ray in the direction of the pointer.

## Pointer.orientation

```csharp
Quat orientation
```

Orientation of the pointer! Since a Ray has no concept
of 'up', this can be used to retrieve more orientation information.

## Pointer.Pose

```csharp
Pose Pose{ get }
```

Convenience property that turns ray.position and orientation
into a Pose.
# struct Mouse

This stores information about the mouse! What's its state, where's
it pointed, do we even have one?

## Mouse.available

```csharp
bool available
```

Is the mouse available to use? Most MR systems likely won't have
a mouse!

## Mouse.pos

```csharp
Vec2 pos
```

Position of the mouse relative to the window it's in! This is the number
of pixels from the top left corner of the screen.

## Mouse.posChange

```csharp
Vec2 posChange
```

How much has the mouse's position changed in the current frame? Measured
in pixels.

## Mouse.scroll

```csharp
float scroll
```

What's the current scroll value for the mouse's scroll wheel? TODO: Units

## Mouse.scrollChange

```csharp
float scrollChange
```

How much has the scroll wheel value changed during this frame? TODO: Units
# enum FingerId

Index values for each finger! From 0-4, from thumb to little finger.

## FingerId.Thumb

```csharp
static FingerId Thumb
```

Finger 0.

## FingerId.Index

```csharp
static FingerId Index
```

The primary index/pointer finger! Finger 1.

## FingerId.Middle

```csharp
static FingerId Middle
```

Finger 2, next to the index finger.

## FingerId.Ring

```csharp
static FingerId Ring
```

Finger 3! What does one do with this finger? I guess... wear
rings on it?

## FingerId.Little

```csharp
static FingerId Little
```

Finger 4, the smallest little finger! AKA, The Pinky.
# enum JointId

Here's where hands get crazy! Technical terms, and watch out for
the thumbs!

## JointId.Root

```csharp
static JointId Root
```

Joint 0. This is at the base of the hand, right above the wrist. For
the thumb, Root and KnuckleMajor have the same value.

## JointId.KnuckleMajor

```csharp
static JointId KnuckleMajor
```

Joint 1. These are the knuckles at the top of the palm! For
the thumb, Root and KnuckleMajor have the same value.

## JointId.KnuckleMid

```csharp
static JointId KnuckleMid
```

Joint 2. These are the knuckles in the middle of the finger! First
joints on the fingers themselves.

## JointId.KnuckleMinor

```csharp
static JointId KnuckleMinor
```

Joint 3. The joints right below the fingertip!

## JointId.Tip

```csharp
static JointId Tip
```

Joint 4. The end/tip of each finger!
# enum UIMove

This describes how a UI element moves when being dragged
around by a user!

## UIMove.Exact

```csharp
static UIMove Exact
```

The element follows the position and orientation of the
user's hand exactly.

## UIMove.FaceUser

```csharp
static UIMove FaceUser
```

The element follows the position of the user's hand, but
orients to face the user's head instead of just using the hand's
rotation.

## UIMove.PosOnly

```csharp
static UIMove PosOnly
```

This element follows the hand's position only, completely
discarding any rotation information.

## UIMove.None

```csharp
static UIMove None
```

Do not allow user input to change the element's pose at
all! You may also be interested in UI.Push/PopSurface.
# enum UIWin

A description of what type of window to draw! This is a bit
flag, so it can contain multiple elements.

## UIWin.Normal

```csharp
static UIWin Normal
```

A normal window has a head and a body to it. Both can be
grabbed.

## UIWin.Empty

```csharp
static UIWin Empty
```

No body, no head. Not really a flag, just set to this
value. The Window will still be grab/movable. To prevent it from
being grabbable, combine with the UIMove.None option, or switch
to UI.Push/PopSurface.

## UIWin.Head

```csharp
static UIWin Head
```

Flag to include a head on the window.

## UIWin.Body

```csharp
static UIWin Body
```

Flag to include a body on the window.
# enum UIConfirm

Used with StereoKit's UI, and determines the interaction
confirmation behavior for certain elements, such as the UI.HSlider!

## UIConfirm.Push

```csharp
static UIConfirm Push
```

The user must push a button with their finger to confirm
interaction with this element. This is simpler to activate as it
requires no learned gestures, but may result in more false
positives.

## UIConfirm.Pinch

```csharp
static UIConfirm Pinch
```

The user must use a pinch gesture to interact with this
element. This is much harder to activate by accident, but does
require the user to make a precise pinch gesture. You can pretty
much be sure that's what the user meant to do!

## UIConfirm.VariablePinch

```csharp
static UIConfirm VariablePinch
```

HSlider specific. Same as Pinch, but pulling out from the
slider creates a scaled slider that lets you adjust the slider at a
more granular resolution.
# enum UIVisual

Used with StereoKit's UI to indicate a particular type of UI
element visual.

## UIVisual.None

```csharp
static UIVisual None
```

Default state, no UI element at all.

## UIVisual.Default

```csharp
static UIVisual Default
```

A default root UI element. Not a particular element, but
other elements may refer to this if there is nothing more specific
present.

## UIVisual.Button

```csharp
static UIVisual Button
```

Refers to UI.Button elements.

## UIVisual.Toggle

```csharp
static UIVisual Toggle
```

Refers to UI.Toggle elements.

## UIVisual.Input

```csharp
static UIVisual Input
```

Refers to UI.Input elements.

## UIVisual.Handle

```csharp
static UIVisual Handle
```

Refers to UI.Handle/HandleBegin elements.

## UIVisual.WindowBody

```csharp
static UIVisual WindowBody
```

Refers to UI.Window/WindowBegin body panel element, this
element is used when a Window head is also present.

## UIVisual.WindowBodyOnly

```csharp
static UIVisual WindowBodyOnly
```

Refers to UI.Window/WindowBegin body element, this element
is used when a Window only has the body panel, without a head.

## UIVisual.WindowHead

```csharp
static UIVisual WindowHead
```

Refers to UI.Window/WindowBegin head panel element, this
element is used when a Window body is also present.

## UIVisual.WindowHeadOnly

```csharp
static UIVisual WindowHeadOnly
```

Refers to UI.Window/WindowBegin head element, this element
is used when a Window only has the head panel, without a body.

## UIVisual.Separator

```csharp
static UIVisual Separator
```

Refers to UI.HSeparator element.

## UIVisual.SliderLine

```csharp
static UIVisual SliderLine
```

Refers to the back line component of the UI.HSlider
element.

## UIVisual.SliderPush

```csharp
static UIVisual SliderPush
```

Refers to the push button component of the UI.HSlider
element when using UIConfirm.Push.

## UIVisual.SliderPinch

```csharp
static UIVisual SliderPinch
```

Refers to the pinch button component of the UI.HSlider
element when using UIConfirm.Pinch.

## UIVisual.Max

```csharp
static UIVisual Max
```

A maximum enum value to allow for iterating through enum
values.
# enum UIColor

Theme color categories to pair with `UI.SetThemeColor`.

## UIColor.Primary

```csharp
static UIColor Primary
```

This is the main accent color used by window headers,
separators, etc.

## UIColor.Background

```csharp
static UIColor Background
```

This is a background sort of color that should generally
be dark. Used by window bodies and backgrounds of certain elements.

## UIColor.Common

```csharp
static UIColor Common
```

A normal UI element color, for elements like buttons and
sliders.

## UIColor.Complement

```csharp
static UIColor Complement
```

Not really used anywhere at the moment, maybe for the
UI.Panel.

## UIColor.Text

```csharp
static UIColor Text
```

Text color! This should generally be really bright, and at
the very least contrast-ey.

## UIColor.Max

```csharp
static UIColor Max
```

A maximum enum value to allow for iterating through enum
values.
# enum UIPad

This specifies a particular padding mode for certain UI
elements, such as the UI.Panel! This describes where padding is applied
and how it affects the layout of elements.

## UIPad.None

```csharp
static UIPad None
```

No padding, this matches the element's layout bounds
exactly!

## UIPad.Inside

```csharp
static UIPad Inside
```

This applies padding inside the element's layout bounds,
and will inflate the layout bounds to fit the extra padding.

## UIPad.Outside

```csharp
static UIPad Outside
```

This will apply the padding outside of the layout bounds!
This will maintain the size and position of the layout volume, but
the visual padding will go outside of the volume.
# enum UIBtnLayout

Describes the layout of a button with image/text contents! You
can think of the naming here as being the location of the image, with
the text filling the remaining space.

## UIBtnLayout.Left

```csharp
static UIBtnLayout Left
```

Image to the left, text to the right. Image will take up
no more than half the width.

## UIBtnLayout.Right

```csharp
static UIBtnLayout Right
```

Image to the right, text to the left. Image will take up
no more than half the width.

## UIBtnLayout.Center

```csharp
static UIBtnLayout Center
```

Image will be centered in the button, and fill up the
button as though it was the only element. Text will cram itself
under the padding below the image.

## UIBtnLayout.CenterNoText

```csharp
static UIBtnLayout CenterNoText
```

Same as `Center`, but omitting the text.
# static class SK

This class contains functions for running the StereoKit
library!

## SK.Settings

```csharp
static SKSettings Settings{ get set }
```

This is a copy of the settings that StereoKit was
initialized with, so you can refer back to them a little easier.
These are read only, and keep in mind that some settings are
only requests! Check SK.System and other properties for the
current state of StereoKit.

## SK.IsInitialized

```csharp
static bool IsInitialized{ get set }
```

Has StereoKit been successfully initialized already? If
initialization was attempted and failed, this value will be
false.

## SK.ActiveDisplayMode

```csharp
static DisplayMode ActiveDisplayMode{ get }
```

Since we can fallback to a different DisplayMode, this
lets you check to see which Runtime was successfully initialized.

## SK.System

```csharp
static SystemInfo System{ get }
```

This structure contains information about the current
system and its capabilities. There's a lot of different MR devices,
so it's nice to have code for systems with particular
characteristics!

## SK.VersionName

```csharp
static string VersionName{ get }
```

Human-readable version name embedded in the StereoKitC
DLL.

## SK.VersionId

```csharp
static UInt64 VersionId{ get }
```

An integer version Id! This is defined using a hex value
with this format: `0xMMMMiiiiPPPPrrrr` in order of
Major.mInor.Patch.pre-Release

## SK.AppFocus

```csharp
static AppFocus AppFocus{ get }
```

This tells about the app's current focus state,
whether it's active and receiving input, or if it's
backgrounded or hidden. This can be important since apps may
still run and render when unfocused, as the app may still be
visible behind the app that _does_ have focus.

## SK.Initialize
```csharp
static bool Initialize(SKSettings settings)
```
Initializes StereoKit window, default resources, systems,
etc.

|  |  |
|--|--|
|SKSettings settings|The configuration settings for StereoKit.             This defines how StereoKit starts up and behaves, so it contains             things like app name, assets folder, display mode, etc. The              default settings are meant to be good for most, but you may want             to modify at least a few of these eventually!|
|RETURNS: bool|Returns true if all systems are successfully initialized!|
```csharp
static bool Initialize(string projectName, string assetsFolder)
```
This is a _very_ rudimentary way to initialize StereoKit,
it doesn't take much, but a robust application will prefer to use
an overload that takes SKSettings. This uses all the default
settings, which are primarily configured for development.

|  |  |
|--|--|
|string projectName|Name of the application, this shows up an             the top of the Win32 window, and is submitted to OpenXR. OpenXR             caps this at 128 characters.|
|string assetsFolder|Where to look for assets when loading             files! Final path will look like '[assetsFolder]/[file]', so a             trailing '/' is unnecessary.|
|RETURNS: bool|Returns true if all systems are successfully initialized!|


## SK.PreLoadLibrary
```csharp
static void PreLoadLibrary()
```
If you need to call StereoKit code before calling
SK.Initialize, you may need to explicitly load the library first.
This can be useful for setting up a few things, but should
probably be a pretty rare case.


## SK.SetWindow
```csharp
static void SetWindow(IntPtr window)
```
Android only. This is for telling StereoKit about the
active Android window surface. In particular, Xamarin's
ISurfaceHolderCallback2 gets SurfaceCreated and SurfaceDestroyed
events, and these events should feed into this function.

|  |  |
|--|--|
|IntPtr window|This is an ISurfaceHolder.Surface.Handle or             equivalent pointer.|


## SK.Shutdown
```csharp
static void Shutdown()
```
Shuts down all StereoKit initialized systems. Release
your own StereoKit created assets before calling this.


## SK.Quit
```csharp
static void Quit()
```
Lets StereoKit know it should quit! It'll finish the
current frame, and after that Step will return that it wants to
exit.


## SK.Step
```csharp
static bool Step(Action onStep)
```
Steps all StereoKit systems, and inserts user code via
callback between the appropriate system updates.

|  |  |
|--|--|
|Action onStep|A callback where you put your application              code! This gets called between StereoKit systems, after frame              setup, but before render.|
|RETURNS: bool|If an exit message is received from the platform, this function will return false.|


## SK.Run
```csharp
static void Run(Action onStep, Action onShutdown)
```
This passes application execution over to StereoKit.
This continuously steps all StereoKit systems, and inserts user
code via callback between the appropriate system updates. Once
execution completes, it properly calls the shutdown callback and
shuts down StereoKit for you.

Using this method is important for compatibility with WASM and is
the preferred method of controlling the main loop, over
`SK.Step`.

|  |  |
|--|--|
|Action onStep|A callback where you put your application              code! This gets called between StereoKit systems, after frame              setup, but before render.|
|Action onShutdown|A callback that gives you the             opportunity to shut things down while StereoKit is still active.             This is called after the last Step completes, and before             StereoKit shuts down.|


## SK.AddStepper
```csharp
static Object AddStepper(Type type)
```
This creates and registers an instance the `IStepper` type
provided as the generic parameter. SK will hold onto it, Initialize
it, Step it every frame, and call Shutdown when the application
ends. This is generally safe to do before SK.Initialize is called,
the constructor is called right away, and Initialize is called
right after SK.Initialize, or right away if SK is already
initialized.

|  |  |
|--|--|
|Type type|Any object that implements IStepper, and has a             constructor with zero parameters.|
|RETURNS: Object|Just for convenience, this returns the instance that was just added.|


## SK.RemoveStepper
```csharp
static void RemoveStepper(IStepper stepper)
```
This removes a specific IStepper from SK's IStepper list.
This will call the IStepper's Shutdown method before returning.

|  |  |
|--|--|
|IStepper stepper|The specific IStepper instance to remove.|
```csharp
static void RemoveStepper(Type type)
```
This removes all IStepper instances that are assignable to
the generic type specified. This will call the IStepper's Shutdown
method on each removed instance before returning.

|  |  |
|--|--|
|Type type|Any type.|


## SK.ExecuteOnMain
```csharp
static void ExecuteOnMain(Action action)
```
This will queue up some code to be run on StereoKit's main
thread! Immediately after StereoKit's Step, all callbacks
registered here will execute, and then removed from the list.

|  |  |
|--|--|
|Action action|Some code to run! This Action will persist in             a list until after Step, at which point it is removed and dropped.|

# static class Assets

If you want to manage loading assets, this is the class for
you!

## Assets.CurrentTask

```csharp
static int CurrentTask{ get }
```

This is the index of the current asset loading task. Note
that to load one asset, multiple tasks are generated.

## Assets.TotalTasks

```csharp
static int TotalTasks{ get }
```

This is the total number of tasks that have been added to
the loading system, including all completed and pending tasks. Note
that to load one asset, multiple tasks are generated.

## Assets.CurrentTaskPriority

```csharp
static int CurrentTaskPriority{ get }
```

StereoKit processes tasks in order of priority. This
returns the priority of the current task, and can be used to wait
until all tasks within a certain priority range have been
completed.

## Assets.BlockForPriority
```csharp
static void BlockForPriority(int priority)
```
This will block the execution of the application until
all asset tasks below the priority value have completed loading.
To block until all assets are loaded, pass in int.MaxValue for the
priority.

|  |  |
|--|--|
|int priority|Block the app until this priority level is             complete.|


## Assets.ModelFormats

```csharp
static String[] ModelFormats
```

A list of supported model format extensions. This pairs
pretty well with `Platform.FilePicker` when attempting to load a
`Model`!

## Assets.TextureFormats

```csharp
static String[] TextureFormats
```

A list of supported texture format extensions. This pairs
pretty well with `Platform.FilePicker` when attempting to load a
`Tex`!
# static class Backend

This class exposes some of StereoKit's backend functionality.
This allows for tighter integration with certain platforms, but also
means your code becomes less portable. Everything in this class should
be guarded by availability checks.

## Backend.XRType

```csharp
static BackendXRType XRType{ get }
```

What technology is being used to drive StereoKit's XR
functionality? OpenXR is the most likely candidate here, but if
you're running the flatscreen Simulator, or running in the web with
WebXR, then this will reflect that.

## Backend.Platform

```csharp
static BackendPlatform Platform{ get }
```

What kind of platform is StereoKit running on? This can be
important to tell you what APIs or functionality is available to
the app.

## Backend.Graphics

```csharp
static BackendGraphics Graphics{ get }
```

This describes the graphics API thatStereoKit is using for
rendering. StereoKit uses D3D11 for Windows platforms, and a flavor
of OpenGL for Linux, Android, and Web.
# static class Backend.OpenXR

This class is NOT of general interest, unless you are
trying to add support for some unusual OpenXR extension! StereoKit
should do all the OpenXR work that most people will need. If you
find yourself here anyhow for something you feel StereoKit should
support already, please add a feature request on GitHub!

This class contains handles and methods for working directly with
OpenXR. This may allow you to activate or work with OpenXR
extensions that StereoKit hasn't implemented or exposed yet. Check
that Backend.XRType is OpenXR before using any of this.

These properties may best be used with some external OpenXR
binding library, but you may get some limited mileage with the API
as provided here.

## Backend.OpenXR.Instance

```csharp
static UInt64 Instance{ get }
```

Type: XrInstance. StereoKit's instance handle, valid
after SK.Initialize.

## Backend.OpenXR.Session

```csharp
static UInt64 Session{ get }
```

Type: XrSession. StereoKit's current session handle,
this will be valid after SK.Initialize, but the session may not
be started quite so early.

## Backend.OpenXR.Space

```csharp
static UInt64 Space{ get }
```

Type: XrSpace. StereoKit's primary coordinate space,
valid after SK.Initialize, this will most likely be created
from `XR_REFERENCE_SPACE_TYPE_UNBOUNDED_MSFT` or
`XR_REFERENCE_SPACE_TYPE_LOCAL`.

## Backend.OpenXR.Time

```csharp
static Int64 Time{ get }
```

Type: XrTime. This is the OpenXR time for the current
frame, and is available after SK.Initialize.

## Backend.OpenXR.EyesSampleTime

```csharp
static Int64 EyesSampleTime{ get }
```

Type: XrTime. This is the OpenXR time of the eye tracker
sample associated with the current value of .

## Backend.OpenXR.ExtEnabled
```csharp
static bool ExtEnabled(string extensionName)
```
This tells if an OpenXR extension has been requested
and successfully loaded by the runtime. This MUST only be
called after SK.Initialize.

|  |  |
|--|--|
|string extensionName|The extension name as listed in the             OpenXR spec. For example: "XR_EXT_hand_tracking".|
|RETURNS: bool|If the extension is available to use.|


## Backend.OpenXR.GetFunctionPtr
```csharp
static IntPtr GetFunctionPtr(string functionName)
```
This is basically `xrGetInstanceProcAddr` from OpenXR,
you can use this to get and call functions from an extension
you've loaded. You can use `Marshal.GetDelegateForFunctionPointer`
to turn the result into a delegate that you can call.

|  |  |
|--|--|
|string functionName||
|RETURNS: IntPtr|A function pointer, or null on failure. You can use `Marshal.GetDelegateForFunctionPointer` to turn this into a delegate that you can call.|


## Backend.OpenXR.GetFunction
```csharp
static TDelegate GetFunction(string functionName)
```
This is basically `xrGetInstanceProcAddr` from OpenXR,
you can use this to get and call functions from an extension
you've loaded. This uses `Marshal.GetDelegateForFunctionPointer`
to turn the result into a delegate that you can call.

|  |  |
|--|--|
|string functionName||
|RETURNS: TDelegate|A delegate, or null on failure.|


## Backend.OpenXR.RequestExt
```csharp
static void RequestExt(string extensionName)
```
Requests that OpenXR load a particular extension. This
MUST be called before SK.Initialize. Note that it's entirely
possible that your extension will not load on certain runtimes,
so be sure to check ExtEnabled to see if it's available to use.

|  |  |
|--|--|
|string extensionName|The extension name as listed in the             OpenXR spec. For example: "XR_EXT_hand_tracking".|


## Backend.OpenXR.AddCompositionLayer
```csharp
static void AddCompositionLayer(T XrCompositionLayerX, int sortOrder)
```
This allows you to add XrCompositionLayers to the list
that StereoKit submits to xrEndFrame. You must call this every
frame you wish the layer to be included.

|  |  |
|--|--|
|T XrCompositionLayerX|A serializable             XrCompositionLayer struct that follows the             XrCompositionLayerBaseHeader data pattern.|
|int sortOrder|An sort order value for sorting with             other composition layers in the list. The primary projection             layer that StereoKit renders to is at 0, -1 would be before it,             and +1 would be after.|

# static class Backend.Android

This class contains variables that may be useful for
interop with the Android operating system, or other Android
libraries.

## Backend.Android.JavaVM

```csharp
static IntPtr JavaVM{ get }
```

This is the `JavaVM*` object that StereoKit uses on
Android. This is only valid after SK.Initialize, on Android
systems.

## Backend.Android.Activity

```csharp
static IntPtr Activity{ get }
```

This is the `jobject` activity that StereoKit uses on
Android. This is only valid after SK.Initialize, on Android
systems.

## Backend.Android.JNIEnvironment

```csharp
static IntPtr JNIEnvironment{ get }
```

This is the `JNIEnv*` object that StereoKit uses on
Android. This is only valid after SK.Initialize, on Android
systems.
# static class Backend.D3D11

When using Direct3D11 for rendering, this contains a
number of variables that may be useful for doing advanced rendering
tasks.

## Backend.D3D11.D3DDevice

```csharp
static IntPtr D3DDevice{ get }
```

This is the main `ID3D11Device*` StereoKit uses for
rendering.

## Backend.D3D11.D3DContext

```csharp
static IntPtr D3DContext{ get }
```

This is the main `ID3D11DeviceContext*` StereoKit uses
for rendering.
# static class Hierarchy

This class represents a stack of transform matrices that
build up a transform hierarchy! This can be used like an object-less
parent-child system, where you push a parent's transform onto the
stack, render child objects relative to that parent transform and
then pop it off the stack.

Performance note: if any matrices are on the hierarchy stack, any
render will cause a matrix multiplication to occur! So if you have a
collection of objects with their transforms baked and cached into
matrices for performance reasons, you'll want to ensure there are no
matrices in the hierarchy stack, or that the hierarchy is disabled!
It'll save you a matrix multiplication in that case :)

## Hierarchy.Push
```csharp
static void Push(Matrix& parentTransform)
```
Pushes a transform Matrix onto the stack, and combines
it with the Matrix below it. Any draw operation's Matrix will now
be combined with this Matrix to make it relative to the current
hierarchy. Use Hierarchy.Pop to remove it from the Hierarchy
stack! All Push calls must have an accompanying Pop call.

|  |  |
|--|--|
|Matrix& parentTransform|The transform Matrix you want to              apply to all following draw calls.|


## Hierarchy.Pop
```csharp
static void Pop()
```
Removes the top Matrix from the stack!


## Hierarchy.Enabled

```csharp
static bool Enabled{ get set }
```

This is enabled by default. Disabling this will cause
any draw call to ignore any Matrices that are on the Hierarchy
stack.

## Hierarchy.ToLocal
```csharp
static Vec3 ToLocal(Vec3 worldPoint)
```
Converts a world space point into the local space of the
current Hierarchy stack!

|  |  |
|--|--|
|Vec3 worldPoint|A point in world space.|
|RETURNS: Vec3|The provided point now in local hierarchy space!|
```csharp
static Quat ToLocal(Quat worldOrientation)
```
Converts a world space rotation into the local space of
the current Hierarchy stack!

|  |  |
|--|--|
|Quat worldOrientation|A rotation in world space.|
|RETURNS: Quat|The provided rotation now in local hierarchy space!|
```csharp
static Pose ToLocal(Pose worldPose)
```
Converts a world pose relative to the current
hierarchy stack into local space!

|  |  |
|--|--|
|Pose worldPose|A pose in world space.|
|RETURNS: Pose|The provided pose now in world space!|


## Hierarchy.ToLocalDirection
```csharp
static Vec3 ToLocalDirection(Vec3 worldDirection)
```
Converts a world space direction into the local space of
the current Hierarchy stack! This excludes the translation
component normally applied to vectors, so it's still a valid
direction.

|  |  |
|--|--|
|Vec3 worldDirection|A direction in world space.|
|RETURNS: Vec3|The provided direction now in local hierarchy space!|


## Hierarchy.ToWorld
```csharp
static Vec3 ToWorld(Vec3 localPoint)
```
Converts a local point relative to the current hierarchy
stack into world space!

|  |  |
|--|--|
|Vec3 localPoint|A point in local space.|
|RETURNS: Vec3|The provided point now in world space!|
```csharp
static Quat ToWorld(Quat localOrientation)
```
Converts a local rotation relative to the current
hierarchy stack into world space!

|  |  |
|--|--|
|Quat localOrientation|A rotation in local space.|
|RETURNS: Quat|The provided rotation now in world space!|
```csharp
static Pose ToWorld(Pose localPose)
```
Converts a local pose relative to the current
hierarchy stack into world space!

|  |  |
|--|--|
|Pose localPose|A pose in local space.|
|RETURNS: Pose|The provided pose now in world space!|


## Hierarchy.ToWorldDirection
```csharp
static Vec3 ToWorldDirection(Vec3 localDirection)
```
Converts a local direction relative to the current
hierarchy stack into world space! This excludes the translation
component normally applied to vectors, so it's still a valid
direction.

|  |  |
|--|--|
|Vec3 localDirection|A direction in local space.|
|RETURNS: Vec3|The provided direction now in world space!|

# struct HandJoint

Contains information to represents a joint on the hand.

## HandJoint.position

```csharp
Vec3 position
```

The center of the joint's world space location.

## HandJoint.orientation

```csharp
Quat orientation
```

The joint's world space orientation, where Forward
points to the next joint down the finger, and Up will point
towards the back of the hand. On the left hand, Right will point
towards the thumb, and on the right hand, Right will point away
from the thumb.

## HandJoint.radius

```csharp
float radius
```

The distance, in meters, to the surface of the hand from
this joint.

## HandJoint.Pose

```csharp
Pose Pose{ get }
```

Pose position is the center of the joint's world space
location. Pose orientation is the world space orientation, where
Forward points to the next joint down the finger. On the left
hand, Right will point towards the thumb, and on the right hand,
Right will point away from the thumb.

## HandJoint.HandJoint
```csharp
void HandJoint(Vec3 position, Quat orientation, float radius)
```
You can make a hand joint of your own here, but most
likely you'd rather fetch one from `Input.Hand().Get()`!

|  |  |
|--|--|
|Vec3 position|The center of the joint's world space              location.|
|Quat orientation|The joint's world space orientation,             where Forward points to the next joint down the finger, and Up             will point towards the back of the hand. On the left hand, Right             will point towards the thumb, and on the right hand, Right will             point away from the thumb.|
|float radius|The distance, in meters, to the surface of             the hand from this joint.|

# class Hand

Information about a hand!

## Hand.fingers

```csharp
HandJoint[] fingers
```

This is a 2D array with 25 HandJoints. You can get the
right joint by `finger*5 + joint`, but really, just use Hand.Get
or Hand[] instead. See Hand.Get for more info!

## Hand.wrist

```csharp
Pose wrist
```

Pose of the wrist. TODO: Not populated right now.

## Hand.palm

```csharp
Pose palm
```

The position and orientation of the palm! Position is
specifically defined as the middle of the middle finger's root
(metacarpal) bone. For orientation, Forward is the direction the
flat of the palm is facing, "Iron Man" style. X+ is to the outside
of the right hand, and to the inside of the left hand.

## Hand.pinchPt

```csharp
Vec3 pinchPt
```

This is an approximation of where the center of a
'pinch' gesture occurs, and is used internally by StereoKit for
some tasks, such as UI. For simulated hands, this position will
give you the most stable pinch location possible. For real hands,
it'll be pretty close to the stablest point you'll get. This is
especially important for when the user begins and ends their
pinch action, as you'll often see a lot of extra movement in the
fingers then.

## Hand.handed

```csharp
Handed handed
```

Is this a right hand, or a left hand?

## Hand.tracked

```csharp
BtnState tracked
```

Is the hand being tracked by the sensors right now?

## Hand.pinch

```csharp
BtnState pinch
```

Is the hand making a pinch gesture right now? Finger and
thumb together.

## Hand.grip

```csharp
BtnState grip
```

Is the hand making a grip gesture right now? Fingers
next to the palm.

## Hand.size

```csharp
float size
```

This is the size of the hand, calculated by measuring
the length of the middle finger! This is calculated by adding the
distances between each joint, then adding the joint radius of the
root and tip. This value is recalculated at relatively frequent
intervals, and can vary by as much as a centimeter.

## Hand.pinchActivation

```csharp
float pinchActivation
```

What percentage of activation is the pinch gesture right
now? Where 0 is a hand in an outstretched resting position, and 1
is fingers touching, within a device error tolerant threshold.

## Hand.gripActivation

```csharp
float gripActivation
```

What percentage of activation is the grip gesture right
now? Where 0 is a hand in an outstretched resting position, and 1
is ring finger touching the base of the palm, within a device
error tolerant threshold.

## Hand.Get
```csharp
HandJoint Get(FingerId finger, JointId joint)
```
Returns the joint information of the indicated hand
joint! This also includes fingertips as a 'joint'. This is the
same as the [] operator. Note that for thumbs, there are only 4
'joints' in reality, so StereoKit has JointId.Root and
JointId.KnuckleMajor as the same pose, so JointId.Tip is still
the tip of the thumb!

|  |  |
|--|--|
|FingerId finger|Which finger are we getting from here, 0 is             thumb, and pinky is 4!|
|JointId joint|Which joint on the finger are we getting? 0              is the root, which is all the way at the base of the palm, and 4             is the tip, the very end of the finger.|
|RETURNS: HandJoint|Position, orientation, and radius of the finger joint.|
```csharp
HandJoint Get(int finger, int joint)
```
Returns the joint information of the indicated hand
joint! This also includes fingertips as a 'joint'. This is the
same as the [] operator. Note that for thumbs, there are only 4
'joints' in reality, so StereoKit has JointId.Root and
JointId.KnuckleMajor as the same pose, so JointId.Tip is still
the tip of the thumb!

|  |  |
|--|--|
|int finger|Which finger are we getting from here, 0 is             thumb, and pinky is 4!|
|int joint|Which joint on the finger are we getting? 0              is the root, which is all the way at the base of the palm, and 4             is the tip, the very end of the finger.|
|RETURNS: HandJoint|Position, orientation, and radius of the finger joint.|


## Hand.IsPinched

```csharp
bool IsPinched{ get }
```

Are the fingers currently pinched?

## Hand.IsJustPinched

```csharp
bool IsJustPinched{ get }
```

Have the fingers just been pinched this frame?

## Hand.IsJustUnpinched

```csharp
bool IsJustUnpinched{ get }
```

Have the fingers just stopped being pinched this frame?

## Hand.IsGripped

```csharp
bool IsGripped{ get }
```

Are the fingers currently gripped?

## Hand.IsJustGripped

```csharp
bool IsJustGripped{ get }
```

Have the fingers just been gripped this frame?

## Hand.IsJustUngripped

```csharp
bool IsJustUngripped{ get }
```

Have the fingers just stopped being gripped this frame?

## Hand.IsTracked

```csharp
bool IsTracked{ get }
```

Is the hand being tracked by the sensors right now?

## Hand.IsJustTracked

```csharp
bool IsJustTracked{ get }
```

Has the hand just started being tracked this frame?

## Hand.IsJustUntracked

```csharp
bool IsJustUntracked{ get }
```

Has the hand just stopped being tracked this frame?

## Hand.Material

```csharp
Material Material{ set }
```

Set the Material used to render the hand! The default
material uses an offset of 10 to ensure it gets drawn overtop of
other elements.

## Hand.Visible

```csharp
bool Visible{ set }
```

Sets whether or not StereoKit should render this hand
for you. Turn this to false if you're going to render your own,
or don't need the hand itself to be visible.

## Hand.Solid

```csharp
bool Solid{ set }
```

Does StereoKit register the hand with the physics
system? By default, this is true. Right now this is just a single
block collider, but later will involve per-joint colliders!
# class Controller

This represents a physical controller input device! Tracking
information, buttons, analog sticks and triggers! There's also a Menu
button that's tracked separately at Input.ContollerMenu.

## Controller.pose

```csharp
Pose pose
```

The grip pose of the controller. This approximately
represents the center of the hand's position. Check `trackedPos`
and `trackedRot` for the current state of the pose data.

## Controller.aim

```csharp
Pose aim
```

The aim pose of a controller is where the controller
'points' from and to. This is great for pointer rays and far
interactions.

## Controller.tracked

```csharp
BtnState tracked
```

This tells the current tracking state of this controller
overall. If either position or rotation are trackable, then this
will report tracked. Typically, positional tracking will be lost
first, when the controller goes out of view, and rotational
tracking will often remain as long as the controller is still
connected. This is a good way to check if the controller is
connected to the system at all.

## Controller.trackedPos

```csharp
TrackState trackedPos
```

This tells the current tracking state of the
controller's position information. This is often the first part
of tracking data to go, so it can often be good to account for
this on occasions.

## Controller.trackedRot

```csharp
TrackState trackedRot
```

This tells the current tracking state of the
controller's rotational information.

## Controller.stickClick

```csharp
BtnState stickClick
```

This represents the click state of the controller's
analog stick or directional controller.

## Controller.x1

```csharp
BtnState x1
```

The current state of the controller's X1 button.
Depending on the specific hardware, this is the first general
purpose button on the controller. For example, on an Oculus Quest
Touch controller this would represent 'X' on the left controller,
and 'A' on the right controller.

## Controller.x2

```csharp
BtnState x2
```

The current state of the controller's X2 button.
Depending on the specific hardware, this is the second general
purpose button on the controller. For example, on an Oculus Quest
Touch controller this would represent 'Y' on the left controller,
and 'B' on the right controller.

## Controller.trigger

```csharp
float trigger
```

The trigger button at the user's index finger. These
buttons typically have a wide range of activation, so this is
provided as a value from 0.0 -> 1.0, where 0 is no interaction,
and 1 is full interaction. If a controller has binary activation,
this will jump straight from 0 to 1.

## Controller.grip

```csharp
float grip
```

The grip button typically sits under the user's middle
finger. These buttons occasionally have a wide range of
activation, so this is provided as a value from 0.0 -> 1.0, where
0 is no interaction, and 1 is full interaction. If a controller
has binary activation, this will jump straight from 0 to 1.

## Controller.stick

```csharp
Vec2 stick
```

This is the current 2-axis position of the analog stick
or equivalent directional controller. This generally ranges from
-1 to +1 on each axis. This is a raw input, so dead-zones and
similar issues are not accounted for here, unless modified by the
OpenXR platform itself.

## Controller.IsX1Pressed

```csharp
bool IsX1Pressed{ get }
```

Is the controller's X1 button currently pressed?
Depending on the specific hardware, this is the first general
purpose button on the controller. For example, on an Oculus Quest
Touch controller this would represent 'X' on the left controller,
and 'A' on the right controller.

## Controller.IsX1JustPressed

```csharp
bool IsX1JustPressed{ get }
```

Has the controller's X1 button just been pressed this
frame? Depending on the specific hardware, this is the first
general purpose button on the controller. For example, on an
Oculus Quest Touch controller this would represent 'X' on the
left controller, and 'A' on the right controller.

## Controller.IsX1JustUnPressed

```csharp
bool IsX1JustUnPressed{ get }
```

Has the controller's X1 button just been released this
frame? Depending on the specific hardware, this is the first
general purpose button on the controller. For example, on an
Oculus Quest Touch controller this would represent 'X' on the
left controller, and 'A' on the right controller.

## Controller.IsX2Pressed

```csharp
bool IsX2Pressed{ get }
```

Is the controller's X2 button currently pressed?
Depending on the specific hardware, this is the second general
purpose button on the controller. For example, on an Oculus Quest
Touch controller this would represent 'X' on the left controller,
and 'A' on the right controller.

## Controller.IsX2JustPressed

```csharp
bool IsX2JustPressed{ get }
```

Has the controller's X2 button just been pressed this
frame? Depending on the specific hardware, this is the second
general purpose button on the controller. For example, on an
Oculus Quest Touch controller this would represent 'X' on the
left controller, and 'A' on the right controller.

## Controller.IsX2JustUnPressed

```csharp
bool IsX2JustUnPressed{ get }
```

Has the controller's X2 button just been released this
frame? Depending on the specific hardware, this is the second
general purpose button on the controller. For example, on an
Oculus Quest Touch controller this would represent 'X' on the
left controller, and 'A' on the right controller.

## Controller.IsStickClicked

```csharp
bool IsStickClicked{ get }
```

Is the analog stick/directional controller button
currently being actively pressed?

## Controller.IsStickJustClicked

```csharp
bool IsStickJustClicked{ get }
```

Has the analog stick/directional controller button
just been pressed this frame?

## Controller.IsStickJustUnclicked

```csharp
bool IsStickJustUnclicked{ get }
```

Has the analog stick/directional controller button
just been released this frame?

## Controller.IsTracked

```csharp
bool IsTracked{ get }
```

Is the controller being tracked by the sensors right now?

## Controller.IsJustTracked

```csharp
bool IsJustTracked{ get }
```

Has the controller just started being tracked this frame?

## Controller.IsJustUntracked

```csharp
bool IsJustUntracked{ get }
```

Has the controller just stopped being tracked this frame?
# static class Input

Input from the system come from this class! Hands, eyes,
heads, mice and pointers!

## Input.Eyes

```csharp
static Pose Eyes{ get }
```

If the device has eye tracking hardware and the app has
permission to use it, then this is the most recently tracked eye
pose. Check `Input.EyesTracked` to see if the pose is up-to date,
or if it's a leftover!

You can also check `SK.System.eyeTrackingPresent` to see if the
hardware is capable of providing eye tracking.

On Flatscreen when the MR sim is still enabled, then eyes are
emulated using the cursor position when the user holds down Alt.

## Input.EyesTracked

```csharp
static BtnState EyesTracked{ get }
```

If eye hardware is available and app has permission,
then this is the tracking state of the eyes. Eyes may move out of
bounds, hardware may fail to detect eyes, or who knows what else!

On Flatscreen when MR sim is still enabled, this will report
whether the user is simulating eye input with the Alt key.

**Permissions**
- For UWP apps, permissions for eye tracking can be found in the project's .appxmanifest file under Capabilities->Gaze Input.
- For Xamarin apps, you may need to add an entry to your AndroidManifest.xml, refer to your device's documentation for specifics.

## Input.Head

```csharp
static Pose Head{ get }
```

The position and orientation of the user's head! This is
the center point between the user's eyes, NOT the center of the
user's head. Forward points the same way the user's face is
facing.

## Input.Mouse

```csharp
static Mouse Mouse{ get }
```

Information about this system's mouse, or lack thereof!

## Input.Controller
```csharp
static Controller Controller(Handed handed)
```
Gets raw controller input data from the system. Note that
not all buttons provided here are guaranteed to be present on the
user's physical controller. Controllers are also not guaranteed to
be available on the system, and are never simulated.

|  |  |
|--|--|
|Handed handed|The handedness of the controller to get the             state of.|
|RETURNS: Controller|A reference to a class that contains state information about the indicated controller.|


## Input.ControllerMenuButton

```csharp
static BtnState ControllerMenuButton{ get }
```

This is the state of the controller's menu button, this
is not attached to any particular hand, so it's independent of a
left or right controller.

## Input.Hand
```csharp
static Hand Hand(Handed handed)
```
Retrieves all the information about the user's hand!
StereoKit will always provide hand information, however sometimes
that information is simulated, like in the case of a mouse, or
controllers.

Note that this is a copy of the hand information, and it's a good
chunk of data, so it's a good idea to grab it once and keep it
around for the frame, or at least function, rather than asking
for it again and again each time you want to touch something.

|  |  |
|--|--|
|Handed handed|Do you want the left or the right hand?|
|RETURNS: Hand|A copy of the entire set of hand data!|


## Input.HandOverride
```csharp
static void HandOverride(Handed hand, HandJoint[]& joints)
```
This allows you to completely override the hand's pose
information! It is still treated like the user's hand, so this is
great for simulating input for testing purposes. It will remain
overridden until you call Input.HandClearOverride.

|  |  |
|--|--|
|Handed hand|Which hand should be overridden?|
|HandJoint[]& joints|A 2D array of 25 joints that should be used             as StereoKit's hand information. See `Hand.fingers` for more              information.|


## Input.HandClearOverride
```csharp
static void HandClearOverride(Handed hand)
```
Clear out the override status from Input.HandOverride,
and restore the user's control over it again.

|  |  |
|--|--|
|Handed hand|Which hand are we clearing the override on?|


## Input.HandVisible
```csharp
static void HandVisible(Handed hand, bool visible)
```
Sets whether or not StereoKit should render the hand for
you. Turn this to false if you're going to render your own, or
don't need the hand itself to be visible.

|  |  |
|--|--|
|Handed hand|If Handed.Max, this will set the value for              both hands.|
|bool visible|True, StereoKit renders this. False, it             doesn't.|


## Input.HandSolid
```csharp
static void HandSolid(Handed hand, bool solid)
```
Does StereoKit register the hand with the physics
system? By default, this is true. Right now this is just a single
block collider, but later will involve per-joint colliders!

|  |  |
|--|--|
|Handed hand|If Handed.Max, this will set the value for              both hands.|
|bool solid|True? Physics! False? No physics.|


## Input.HandMaterial
```csharp
static void HandMaterial(Handed hand, Material material)
```
Set the Material used to render the hand! The default
material uses an offset of 10 to ensure it gets drawn overtop of
other elements.

|  |  |
|--|--|
|Handed hand|If Handed.Max, this will set the value for              both hands.|
|Material material|The new Material!|


## Input.Key
```csharp
static BtnState Key(Key key)
```
Keyboard key state! On desktop this is super handy, but
even standalone MR devices can have bluetooth keyboards, or even
just holographic system keyboards!

|  |  |
|--|--|
|Key key|The key to get the state of. Any key!|
|RETURNS: BtnState|A BtnState with a number of different bits of info about whether or not the key was pressed or released this frame.|


## Input.TextConsume
```csharp
static Char TextConsume()
```
Returns the next text character from the list of
characters that have been entered this frame! Will return '\0' if
there are no more characters left in the list. These are from the
system's text entry system, and so can be unicode, will repeat if
their 'key' is held down, and could arrive from something like a
copy/paste operation.

If you wish to reset this function to begin at the start of the
read list on the next call, you can call `Input.TextReset`.

|  |  |
|--|--|
|RETURNS: Char|The next character in this frame's list, or '\0' if none remain.|


## Input.TextReset
```csharp
static void TextReset()
```
Resets the `Input.TextConsume` read list back to the
start.
For example, `UI.Input` will _not_ call `TextReset`, so it
effectively will consume those characters, hiding them from
any `TextConsume` calls following it. If you wanted to check the
current frame's text, but still allow `UI.Input` to work later on
in the frame, you would read everything with `TextConsume`, and
then `TextReset` afterwards to reset the read list for the
following `UI.Input`.

# static class Lines

A line drawing class! This is an easy way to visualize lines
or relationships between objects. The current implementation uses a
quad strip that always faces the user, via vertex shader
manipulation.

## Lines.Add
```csharp
static void Add(Vec3 start, Vec3 end, Color32 color, float thickness)
```
Adds a line to the environment for the current frame.

|  |  |
|--|--|
|Vec3 start|Starting point of the line.|
|Vec3 end|End point of the line.|
|Color32 color|Color for the line, this is embedded in the             vertex color of the line.|
|float thickness|Thickness of the line in meters.|
```csharp
static void Add(Vec3 start, Vec3 end, Color32 colorStart, Color32 colorEnd, float thickness)
```
Adds a line to the environment for the current frame.

|  |  |
|--|--|
|Vec3 start|Starting point of the line.|
|Vec3 end|End point of the line.|
|Color32 colorStart|Color for the start of the line, this is             embedded in the vertex color of the line.|
|Color32 colorEnd|Color for the end of the line, this is             embedded in the vertex color of the line.|
|float thickness|Thickness of the line in meters.|
```csharp
static void Add(Ray ray, float length, Color32 color, float thickness)
```
Adds a line based on a ray to the environment for the
current frame.

|  |  |
|--|--|
|Ray ray|The ray we want to visualize!|
|float length|How long should the ray be? Actual length             will be ray.direction.Magnitude * length.|
|Color32 color|Color for the line, this is embedded in the             vertex color of the line.|
|float thickness|Thickness of the line in meters.|
```csharp
static void Add(LinePoint[]& points)
```
Adds a line from a list of line points to the
environment. This does not close the path, so if you want it
closed, you'll have to add an extra point or two at the end
yourself!

|  |  |
|--|--|
|LinePoint[]& points|An array of line points.|


## Lines.AddAxis
```csharp
static void AddAxis(Pose atPose, float size, float thickness)
```
Displays an RGB/XYZ axis widget at the pose! Each line
is extended along the positive direction of each axis, so the red
line is +X, green is +Y, and blue is +Z. A white line is drawn
along -Z to indicate the Forward vector of the pose (-Z is
forward in StereoKit).

|  |  |
|--|--|
|Pose atPose|What position and orientation do we want             this axis widget at?|
|float size|How long should the widget lines be, in             meters?|
|float thickness|How thick should the lines be, in meters?|
```csharp
static void AddAxis(Pose atPose, float size)
```
Displays an RGB/XYZ axis widget at the pose! Each line
is extended along the positive direction of each axis, so the red
line is +X, green is +Y, and blue is +Z. A white line is drawn
along -Z to indicate the Forward vector of the pose (-Z is
forward in StereoKit).

|  |  |
|--|--|
|Pose atPose|What position and orientation do we want             this axis widget at?|
|float size|How long should the widget lines be, in             meters?|

# static class Log

A class for logging errors, warnings and information!
Different levels of information can be filtered out, and supports
coloration via <~[colorCode]> and <~clr> tags.

Text colors can be set with a tag, and reset back to default with
<~clr>. Color codes are as follows:

| Dark | Bright | Description |
|------|--------|-------------|
| DARK | BRIGHT | DESCRIPTION |
| blk  | BLK    | Black       |
| red  | RED    | Red         |
| grn  | GRN    | Green       |
| ylw  | YLW    | Yellow      |
| blu  | BLU    | Blue        |
| mag  | MAG    | Magenta     |
| cyn  | cyn    | Cyan        |
| grn  | GRN    | Green       |
| wht  | WHT    | White       |

## Log.Filter

```csharp
static LogLevel Filter{ set }
```

What's the lowest level of severity logs to display on
the console? Default is LogLevel.Info. This property can safely
be set before SK initialization.

## Log.Write
```csharp
static void Write(LogLevel level, string text, Object[] items)
```
Writes a formatted line to the log with the specified
severity level!

|  |  |
|--|--|
|LogLevel level|Severity level of this log message.|
|string text|Formatted text with color tags! See the Log             class docs for guidance on color tags.|
|Object[] items|Format arguments.|
```csharp
static void Write(LogLevel level, string text)
```
Writes a formatted line to the log with the specified
severity level!

|  |  |
|--|--|
|LogLevel level|Severity level of this log message.|
|string text|Formatted text with color tags! See the Log             class docs for guidance on color tags.|


## Log.Info
```csharp
static void Info(string text, Object[] items)
```
Writes a formatted line to the log using a LogLevel.Info
severity level!

|  |  |
|--|--|
|string text|Formatted text with color tags! See the Log             class docs for guidance on color tags.|
|Object[] items|Format arguments.|
```csharp
static void Info(string text)
```
Writes a formatted line to the log using a LogLevel.Info
severity level!

|  |  |
|--|--|
|string text|Formatted text with color tags! See the Log             class docs for guidance on color tags.|


## Log.Warn
```csharp
static void Warn(string text, Object[] items)
```
Writes a formatted line to the log using a LogLevel.Warn
severity level!

|  |  |
|--|--|
|string text|Formatted text with color tags! See the Log             class docs for guidance on color tags.|
|Object[] items|Format arguments.|
```csharp
static void Warn(string text)
```
Writes a formatted line to the log using a LogLevel.Warn
severity level!

|  |  |
|--|--|
|string text|Formatted text with color tags! See the Log             class docs for guidance on color tags.|


## Log.Err
```csharp
static void Err(string text, Object[] items)
```
Writes a formatted line to the log using a
LogLevel.Error severity level!

|  |  |
|--|--|
|string text|Formatted text with color tags! See the Log             class docs for guidance on color tags.|
|Object[] items|Format arguments.|
```csharp
static void Err(string text)
```
Writes a formatted line to the log using a
LogLevel.Error severity level!

|  |  |
|--|--|
|string text|Formatted text with color tags! See the Log             class docs for guidance on color tags.|


## Log.Subscribe
```csharp
static void Subscribe(LogCallback onLog)
```
Allows you to listen in on log events! Any callback
subscribed here will be called when something is logged. This
does honor the Log.Filter, so filtered logs will not be received
here. This method can safely be called before SK initialization.

|  |  |
|--|--|
|LogCallback onLog|The function to call when a log event occurs.|


## Log.Unsubscribe
```csharp
static void Unsubscribe(LogCallback onLog)
```
If you subscribed to the log callback, you can
unsubscribe that callback here!
This method can safely be called before initialization.

|  |  |
|--|--|
|LogCallback onLog|The subscribed callback to remove.|

# static class Microphone

This class provides access to the hardware's microphone, and
stores it in a Sound stream. Start and Stop recording, and check the
Sound property for the results! Remember to ensure your application
has microphone permissions enabled!

## Microphone.Sound

```csharp
static Sound Sound{ get }
```

This is the sound stream of the Microphone when it is
recording. This Asset is created the first time it is accessed
via this property, or during Start, and will persist. It is
re-used for the Microphone stream if you start/stop/switch
devices.

## Microphone.IsRecording

```csharp
static bool IsRecording{ get }
```

Tells if the Microphone is currently recording audio.

## Microphone.GetDevices
```csharp
static String[] GetDevices()
```
Constructs a list of valid Microphone devices attached
to the system. These names can be passed into Start to select
a specific device to record from. It's recommended to cache this
list if you're using it frequently, as this list is constructed
each time you call it.

It's good to note that a user might occasionally plug or unplug
microphone devices from their system, so this list may
occasionally change.

|  |  |
|--|--|
|RETURNS: String[]|List of human readable microphone device names.|


## Microphone.Start
```csharp
static bool Start(string deviceName)
```
This begins recording audio from the Microphone! Audio
is stored in Microphone.Sound as a stream of audio. If the
Microphone is already recording with a different device, it will
stop the previous recording and start again with the new device.

If null is provided as the device, then they system's default
input device will be used. Some systems may not provide access
to devices other than the system's default.

|  |  |
|--|--|
|string deviceName|The name of the microphone device to             use, as seen in the GetDevices list. null will use the system's             default device preference.|
|RETURNS: bool|True if recording started successfully, false for failure. This could fail if the app does not have mic permissions, or if the deviceName is for a mic that has since been unplugged.|


## Microphone.Stop
```csharp
static void Stop()
```
If the Microphone is recording, this will stop it.

# static class Renderer

Do you need to draw something? Well, you're probably in the right place!
This static class includes a variety of different drawing methods, from rendering
Models and Meshes, to setting rendering options and drawing to offscreen surfaces!
Even better, it's entirely a static class, so you can call it from anywhere :)

## Renderer.SkyTex

```csharp
static Tex SkyTex{ get set }
```

Set a cubemap skybox texture for rendering a background! This is only visible on Opaque
displays, since transparent displays have the real world behind them already! StereoKit has a
a default procedurally generated skybox. You can load one with `Tex.FromEquirectangular`,
`Tex.GenCubemap`. If you're trying to affect the lighting, see `Renderer.SkyLight`.

## Renderer.SkyLight

```csharp
static SphericalHarmonics SkyLight{ get set }
```

Sets the lighting information for the scene! You can
build one through `SphericalHarmonics.FromLights`, or grab one
from `Tex.FromEquirectangular` or `Tex.GenCubemap`

## Renderer.EnableSky

```csharp
static bool EnableSky{ get set }
```

Enables or disables rendering of the skybox texture! It's enabled by default on Opaque
displays, and completely unavailable for transparent displays.

## Renderer.LayerFilter

```csharp
static RenderLayer LayerFilter{ get set }
```

By default, StereoKit renders all layers. This is a bit
flag that allows you to change which layers StereoKit renders for
the primary viewpoint. To change what layers a visual is on, use
a Draw method that includes a RenderLayer as a parameter.

## Renderer.HasCaptureFilter

```csharp
static bool HasCaptureFilter{ get }
```

This tells if CaptureFilter has been overridden to a
specific value via `Renderer.OverrideCaptureFilter`.

## Renderer.CaptureFilter

```csharp
static RenderLayer CaptureFilter{ get }
```

This is the current render layer mask for Mixed Reality
Capture, or 2nd person observer rendering. By default, this is
directly linked to Renderer.LayerFilter, but this behavior can be
overridden via `Renderer.OverrideCaptureFilter`.

## Renderer.ClearColor

```csharp
static Color ClearColor{ get set }
```

This is the gamma space color the renderer will clear
the screen to when beginning to draw a new frame. This is ignored
on displays with transparent screens

## Renderer.CameraRoot

```csharp
static Matrix CameraRoot{ get set }
```

Sets and gets the root transform of the camera! This
will be the identity matrix by default. The user's head  location
will then be relative to this point. This is great to use if
you're trying to do teleportation, redirected walking, or just
shifting the floor around.

## Renderer.Projection

```csharp
static Projection Projection{ get set }
```

For flatscreen applications only! This allows you to
change the camera projection between perspective and orthographic
projection. This may be of interest for some category of UI work,
but is generally a niche piece of functionality.

Swapping between perspective and orthographic will also switch the
clipping planes and field of view to the values associated with
that mode. See `SetClip`/`SetFov` for perspective, and
`SetOrthoClip`/`SetOrthoSize` for orthographic.

## Renderer.OverrideCaptureFilter
```csharp
static void OverrideCaptureFilter(bool useOverrideFilter, RenderLayer overrideFilter)
```
The CaptureFilter is a layer mask for Mixed Reality
Capture, or 2nd person observer rendering. On HoloLens and WMR,
this is the video rendering feature. This allows you to hide, or
reveal certain draw calls when rendering video output.

By default, the CaptureFilter will always be the same as
`Render.LayerFilter`, overriding this will mean this filter no
longer updates with `LayerFilter`.

|  |  |
|--|--|
|bool useOverrideFilter|Enables (true) or disables (false)             the overridden filter value provided here.|
|RenderLayer overrideFilter|The filter for capture rendering to             use. This is ignored if useOverrideFilter is false.|


## Renderer.Add
```csharp
static void Add(Mesh mesh, Material material, Matrix transform)
```
Adds a mesh to the render queue for this frame! If the
Hierarchy has a transform on it, that transform is combined with
the Matrix provided here.

|  |  |
|--|--|
|Mesh mesh|A valid Mesh you wish to draw.|
|Material material|A Material to apply to the Mesh.|
|Matrix transform|A Matrix that will transform the mesh             from Model Space into the current Hierarchy Space.|
```csharp
static void Add(Mesh mesh, Material material, Matrix transform, Color colorLinear, RenderLayer layer)
```
Adds a mesh to the render queue for this frame! If the
Hierarchy has a transform on it, that transform is combined with
the Matrix provided here.

|  |  |
|--|--|
|Mesh mesh|A valid Mesh you wish to draw.|
|Material material|A Material to apply to the Mesh.|
|Matrix transform|A Matrix that will transform the mesh             from Model Space into the current Hierarchy Space.|
|Color colorLinear|A per-instance linear space color value             to pass into the shader! Normally this gets used like a material             tint. If you're  adventurous and don't need per-instance colors,             this is a great spot to pack in extra per-instance data for the             shader!|
|RenderLayer layer|All visuals are rendered using a layer              bit-flag. By default, all layers are rendered, but this can be              useful for filtering out objects for different rendering              purposes! For example: rendering a mesh over the user's head from             a 3rd person perspective, but filtering it out from the 1st             person perspective.|
```csharp
static void Add(Model model, Matrix transform)
```
Adds a Model to the render queue for this frame! If the
Hierarchy has a transform on it, that transform is combined with
the Matrix provided here.

|  |  |
|--|--|
|Model model|A valid Model you wish to draw.|
|Matrix transform|A Matrix that will transform the Model             from Model Space into the current Hierarchy Space.|
```csharp
static void Add(Model model, Matrix transform, Color colorLinear, RenderLayer layer)
```
Adds a Model to the render queue for this frame! If the
Hierarchy has a transform on it, that transform is combined with
the Matrix provided here.

|  |  |
|--|--|
|Model model|A valid Model you wish to draw.|
|Matrix transform|A Matrix that will transform the Model             from Model Space into the current Hierarchy Space.|
|Color colorLinear|A per-instance linear space color value             to pass into the shader! Normally this gets used like a material             tint. If you're  adventurous and don't need per-instance colors,             this is a great spot to pack in extra per-instance data for the             shader!|
|RenderLayer layer|All visuals are rendered using a layer              bit-flag. By default, all layers are rendered, but this can be              useful for filtering out objects for different rendering              purposes! For example: rendering a mesh over the user's head from             a 3rd person perspective, but filtering it out from the 1st             person perspective.|


## Renderer.SetClip
```csharp
static void SetClip(float nearPlane, float farPlane)
```
Set the near and far clipping planes of the camera!
These are important to z-buffer quality, especially when using
low bit depth z-buffers as recommended for devices like the
HoloLens. The smaller the range between the near and far planes,
the better your z-buffer will look! If you see flickering on
objects that are overlapping, try making the range smaller.

These values only affect perspective mode projection, which is the
default projection mode.

|  |  |
|--|--|
|float nearPlane|The GPU discards pixels that are too             close to the camera, this is that distance! It must be larger             than zero, due to the projection math, which also means that             numbers too close to zero will produce z-fighting artifacts. This             has an enforced minimum of 0.001, but you should probably stay             closer to 0.1.|
|float farPlane|At what distance from the camera does the             GPU discard pixel? This is not true distance, but rather Z-axis             distance from zero in View Space coordinates!|


## Renderer.SetFOV
```csharp
static void SetFOV(float fieldOfViewDegrees)
```
Only works for flatscreen! This updates the camera's
projection matrix with a new field of view.

This value only affects perspective mode projection, which is the
default projection mode.

|  |  |
|--|--|
|float fieldOfViewDegrees|Vertical field of view in degrees.|


## Renderer.SetOrthoClip
```csharp
static void SetOrthoClip(float nearPlane, float farPlane)
```
Set the near and far clipping planes of the camera!
These are important to z-buffer quality, especially when using
low bit depth z-buffers as recommended for devices like the
HoloLens. The smaller the range between the near and far planes,
the better your z-buffer will look! If you see flickering on
objects that are overlapping, try making the range smaller.

These values only affect orthographic mode projection, which is
only available in flatscreen.

|  |  |
|--|--|
|float nearPlane|The GPU discards pixels that are too             close to the camera, this is that distance!|
|float farPlane|At what distance from the camera does the             GPU discard pixel? This is not true distance, but rather Z-axis             distance from zero in View Space coordinates!|


## Renderer.SetOrthoSize
```csharp
static void SetOrthoSize(float viewportHeightMeters)
```
This sets the size of the orthographic projection's
viewport. You can use this feature to zoom in and out of the scene.

This value only affects orthographic mode projection, which is only
available in flatscreen.

|  |  |
|--|--|
|float viewportHeightMeters|The vertical size of the             projection's viewport, in meters.|


## Renderer.Blit
```csharp
static void Blit(Tex toRendertarget, Material material)
```
Renders a Material onto a rendertarget texture! StereoKit uses a 4 vert quad stretched
over the surface of the texture, and renders the material onto it to the texture.

|  |  |
|--|--|
|Tex toRendertarget|A texture that's been set up as a render target!|
|Material material|This material is rendered onto the texture! Set it up like you would             if you were applying it to a plane, or quad mesh.|


## Renderer.Screenshot
```csharp
static void Screenshot(Vec3 from, Vec3 at, int width, int height, string filename)
```
Schedules a screenshot for the end of the frame! The view will be
rendered from the given position at the given point, with a resolution the same
size as the screen's surface. It'll be saved as a .jpg file at the filename
provided.

|  |  |
|--|--|
|Vec3 from|Viewpoint location.|
|Vec3 at|Direction the viewpoint is looking at.|
|int width|Size of the screenshot horizontally, in pixels.|
|int height|Size of the screenshot vertically, in pixels.|
|string filename|Filename to write the screenshot to! Note this'll be a              .jpg regardless of what file extension you use right now.|
```csharp
static void Screenshot(string filename, Vec3 from, Vec3 at, int width, int height, float fieldOfViewDegrees)
```
Schedules a screenshot for the end of the frame! The view
will be rendered from the given position at the given point, with a
resolution the same size as the screen's surface. It'll be saved as
a .jpg file at the filename provided.

|  |  |
|--|--|
|string filename|Filename to write the screenshot to! Note             this'll be a .jpg regardless of what file extension you use right             now.|
|Vec3 from|Viewpoint location.|
|Vec3 at|Direction the viewpoint is looking at.|
|int width|Size of the screenshot horizontally, in pixels.|
|int height|Size of the screenshot vertically, in pixels.|
|float fieldOfViewDegrees|The angle of the viewport, in              degrees.|


## Renderer.RenderTo
```csharp
static void RenderTo(Tex toRendertarget, Matrix camera, Matrix projection, RenderLayer layerFilter, RenderClear clear, Rect viewport)
```
This renders the current scene to the indicated
rendertarget texture, from the specified viewpoint. This call
enqueues a render that occurs immediately before the screen
itself is rendered.

|  |  |
|--|--|
|Tex toRendertarget|The texture to which the scene will             be rendered to. This must be a Rendertarget type texture.|
|Matrix camera|A TRS matrix representing the location and             orientation of the camera. This matrix gets inverted later on, so             no need to do it yourself.|
|Matrix projection|The projection matrix describes how the             geometry is flattened onto the draw surface. Normally, you'd use              Matrix.Perspective, and occasionally Matrix.Orthographic might be             helpful as well.|
|RenderLayer layerFilter|This is a bit flag that allows you to             change which layers StereoKit renders for this particular render             viewpoint. To change what layers a visual is on, use a Draw             method that includes a RenderLayer as a parameter.|
|RenderClear clear|Describes if an how the rendertarget should             be cleared before rendering. Note that clearing the target is             unaffected by the viewport, so this will clean the entire              surface!|
|Rect viewport|Allows you to specify a region of the             rendertarget to draw to! This is in normalized coordinates, 0-1.             If the width of this value is zero, then this will render to the             entire texture.|

# struct TextStyle

A text style is a font plus size/color/material parameters,
and are used to keep text looking more consistent through the
application by encouraging devs to re-use styles throughout the
project. See Text.MakeStyle for making a TextStyle object.

## TextStyle.Material

```csharp
Material Material{ get }
```

This provides a reference to the Material used by this
style, so you can override certain features! Note that if you're
creating TextStyles with manually provided Materials, this
Material may not be unique to this style.

## TextStyle.CharHeight

```csharp
float CharHeight{ get }
```

Returns the maximum height of a text character using
this style, in meters.

## TextStyle.Default

```csharp
static TextStyle Default{ get }
```

This is the default text style used by StereoKit.
# static class Text

A collection of functions for rendering and working with text.
These are a lower level access to text rendering than the UI text
functions, and are completely unaware of the UI code.

## Text.MakeStyle
```csharp
static TextStyle MakeStyle(Font font, float characterHeightMeters, Color colorGamma)
```
Create a text style for use with other text functions! A
text style is a font plus size/color/material parameters, and are
used to keep text looking more consistent through the application
by encouraging devs to re-use styles throughout the project.

This overload will create a unique Material for this style based
on Default.ShaderFont.

|  |  |
|--|--|
|Font font|Font asset you want attached to this style.|
|float characterHeightMeters|Height of a text glyph in             meters. StereoKit currently bases this on the letter 'T'.|
|Color colorGamma|The gamma space color of the text             style. This will be embedded in the vertex color of the text             mesh.|
|RETURNS: TextStyle|A text style id for use with text rendering functions.|
```csharp
static TextStyle MakeStyle(Font font, float characterHeightMeters, Shader shader, Color colorGamma)
```
Create a text style for use with other text functions! A
text style is a font plus size/color/material parameters, and are
used to keep text looking more consistent through the application
by encouraging devs to re-use styles throughout the project.

This overload will create a unique Material for this style based
on the provided Shader.

|  |  |
|--|--|
|Font font|Font asset you want attached to this style.|
|float characterHeightMeters|Height of a text glyph in             meters. StereoKit currently bases this on the letter 'T'.|
|Shader shader|This style will create and use a unique             Material based on the Shader that you provide here.|
|Color colorGamma|The gamma space color of the text             style. This will be embedded in the vertex color of the text             mesh.|
|RETURNS: TextStyle|A text style id for use with text rendering functions.|
```csharp
static TextStyle MakeStyle(Font font, float characterHeightMeters, Material material, Color colorGamma)
```
Create a text style for use with other text functions! A
text style is a font plus size/color/material parameters, and are
used to keep text looking more consistent through the application
by encouraging devs to re-use styles throughout the project.

This overload allows you to set the specific Material that is
used. This can be helpful if you're keeping styles similar enough
to re-use the material and save on draw calls. If you don't know
what that means, then prefer using the overload that takes a
Shader, or takes neither a Shader nor a Material!

|  |  |
|--|--|
|Font font|Font asset you want attached to this style.|
|float characterHeightMeters|Height of a text glyph in             meters. StereoKit currently bases this on the letter 'T'.|
|Material material|Which material should be used to render             the text with? Note that this does NOT duplicate the material, so             some parameters of this Material instance will get overwritten,              like the texture used for the glyph atlas. You should either use             a new Material, or a Material that was already used with this             same font.|
|Color colorGamma|The gamma space color of the text             style. This will be embedded in the vertex color of the text             mesh.|
|RETURNS: TextStyle|A text style id for use with text rendering functions.|


## Text.Add
```csharp
static void Add(string text, Matrix transform, TextStyle style, TextAlign position, TextAlign align, float offX, float offY, float offZ)
```
Renders text at the given location! Must be called every
frame you want this text to be visible.

|  |  |
|--|--|
|string text|What text should be drawn?|
|Matrix transform|A Matrix representing the transform of the             text mesh! Try Matrix.TRS.|
|TextStyle style|Style information for rendering, see             Text.MakeStyle or the TextStyle object.|
|TextAlign position|How should the text's bounding rectangle be             positioned relative to the transform?|
|TextAlign align|How should the text be aligned within the             text's bounding rectangle?|
|float offX|An additional offset on the X axis.|
|float offY|An additional offset on the Y axis.|
|float offZ|An additional offset on the Z axis.|
```csharp
static void Add(string text, Matrix transform, TextAlign position, TextAlign align, float offX, float offY, float offZ)
```
Renders text at the given location! Must be called every
frame you want this text to be visible.

|  |  |
|--|--|
|string text|What text should be drawn?|
|Matrix transform|A Matrix representing the transform of the             text mesh! Try Matrix.TRS.|
|TextAlign position|How should the text's bounding rectangle be             positioned relative to the transform?|
|TextAlign align|How should the text be aligned within the             text's bounding rectangle?|
|float offX|An additional offset on the X axis.|
|float offY|An additional offset on the Y axis.|
|float offZ|An additional offset on the Z axis.|
```csharp
static void Add(string text, Matrix transform, TextStyle style, Color vertexTintLinear, TextAlign position, TextAlign align, float offX, float offY, float offZ)
```
Renders text at the given location! Must be called every
frame you want this text to be visible.

|  |  |
|--|--|
|string text|What text should be drawn?|
|Matrix transform|A Matrix representing the transform of the             text mesh! Try Matrix.TRS.|
|TextStyle style|Style information for rendering, see             Text.MakeStyle or the TextStyle object.|
|TextAlign position|How should the text's bounding rectangle be             positioned relative to the transform?|
|TextAlign align|How should the text be aligned within the             text's bounding rectangle?|
|float offX|An additional offset on the X axis.|
|float offY|An additional offset on the Y axis.|
|float offZ|An additional offset on the Z axis.|
|Color vertexTintLinear|The vertex color of the text gets             multiplied by this color. This is a linear color value, not a gamma             corrected color value.|
```csharp
static void Add(string text, Matrix transform, Color vertexTintLinear, TextAlign position, TextAlign align, float offX, float offY, float offZ)
```
Renders text at the given location! Must be called every
frame you want this text to be visible.

|  |  |
|--|--|
|string text|What text should be drawn?|
|Matrix transform|A Matrix representing the transform of the             text mesh! Try Matrix.TRS.|
|TextAlign position|How should the text's bounding rectangle be             positioned relative to the transform?|
|TextAlign align|How should the text be aligned within the             text's bounding rectangle?|
|float offX|An additional offset on the X axis.|
|float offY|An additional offset on the Y axis.|
|float offZ|An additional offset on the Z axis.|
|Color vertexTintLinear|The vertex color of the text gets             multiplied by this color. This is a linear color value, not a gamma             corrected color value.|
```csharp
static float Add(string text, Matrix transform, Vec2 size, TextFit fit, TextStyle style, TextAlign position, TextAlign align, float offX, float offY, float offZ)
```
Renders text at the given location! Must be called every
frame you want this text to be visible.

|  |  |
|--|--|
|string text|What text should be drawn?|
|Matrix transform|A Matrix representing the transform of the             text mesh! Try Matrix.TRS.|
|Vec2 size|This is the Hierarchy space rectangle that the             text should try to fit inside of. This allows for text wrapping or             scaling based on the value provided to the 'fit' parameter.|
|TextFit fit|Describe how the text should behave when one of             its size dimensions conflicts with the provided 'size' parameter.|
|TextStyle style|Style information for rendering, see             Text.MakeStyle or the TextStyle object.|
|TextAlign position|How should the text's bounding rectangle be             positioned relative to the transform?|
|TextAlign align|How should the text be aligned within the             text's bounding rectangle?|
|float offX|An additional offset on the X axis.|
|float offY|An additional offset on the Y axis.|
|float offZ|An additional offset on the Z axis.|
|RETURNS: float|Returns the vertical space used by this text.|
```csharp
static float Add(string text, Matrix transform, Vec2 size, TextFit fit, TextAlign position, TextAlign align, float offX, float offY, float offZ)
```
Renders text at the given location! Must be called every
frame you want this text to be visible.

|  |  |
|--|--|
|string text|What text should be drawn?|
|Matrix transform|A Matrix representing the transform of the             text mesh! Try Matrix.TRS.|
|Vec2 size|This is the Hierarchy space rectangle that the             text should try to fit inside of. This allows for text wrapping or             scaling based on the value provided to the 'fit' parameter.|
|TextFit fit|Describe how the text should behave when one of             its size dimensions conflicts with the provided 'size' parameter.|
|TextAlign position|How should the text's bounding rectangle be             positioned relative to the transform?|
|TextAlign align|How should the text be aligned within the             text's bounding rectangle?|
|float offX|An additional offset on the X axis.|
|float offY|An additional offset on the Y axis.|
|float offZ|An additional offset on the Z axis.|
|RETURNS: float|Returns the vertical space used by this text.|
```csharp
static float Add(string text, Matrix transform, Vec2 size, TextFit fit, Color vertexTintLinear, TextAlign position, TextAlign align, float offX, float offY, float offZ)
```
Renders text at the given location! Must be called every
frame you want this text to be visible.

|  |  |
|--|--|
|string text|What text should be drawn?|
|Matrix transform|A Matrix representing the transform of the             text mesh! Try Matrix.TRS.|
|Vec2 size|This is the Hierarchy space rectangle that the             text should try to fit inside of. This allows for text wrapping or             scaling based on the value provided to the 'fit' parameter.|
|TextFit fit|Describe how the text should behave when one of             its size dimensions conflicts with the provided 'size' parameter.|
|TextAlign position|How should the text's bounding rectangle be             positioned relative to the transform?|
|TextAlign align|How should the text be aligned within the             text's bounding rectangle?|
|float offX|An additional offset on the X axis.|
|float offY|An additional offset on the Y axis.|
|float offZ|An additional offset on the Z axis.|
|Color vertexTintLinear|The vertex color of the text gets             multiplied by this color. This is a linear color value, not a gamma             corrected color value.|
|RETURNS: float|Returns the vertical space used by this text.|
```csharp
static float Add(string text, Matrix transform, Vec2 size, TextFit fit, TextStyle style, Color vertexTintLinear, TextAlign position, TextAlign align, float offX, float offY, float offZ)
```
Renders text at the given location! Must be called every
frame you want this text to be visible.

|  |  |
|--|--|
|string text|What text should be drawn?|
|Matrix transform|A Matrix representing the transform of the             text mesh! Try Matrix.TRS.|
|Vec2 size|This is the Hierarchy space rectangle that the             text should try to fit inside of. This allows for text wrapping or             scaling based on the value provided to the 'fit' parameter.|
|TextFit fit|Describe how the text should behave when one of             its size dimensions conflicts with the provided 'size' parameter.|
|TextStyle style|Style information for rendering, see             Text.MakeStyle or the TextStyle object.|
|TextAlign position|How should the text's bounding rectangle be             positioned relative to the transform?|
|TextAlign align|How should the text be aligned within the             text's bounding rectangle?|
|float offX|An additional offset on the X axis.|
|float offY|An additional offset on the Y axis.|
|float offZ|An additional offset on the Z axis.|
|Color vertexTintLinear|The vertex color of the text gets             multiplied by this color. This is a linear color value, not a gamma             corrected color value.|
|RETURNS: float|Returns the vertical space used by this text.|


## Text.Size
```csharp
static Vec2 Size(string text, TextStyle style)
```
Sometimes you just need to know how much room some text
takes up! This finds the size of the text in meters when using the
indicated style!

|  |  |
|--|--|
|string text|Text you want to find the size of.|
|TextStyle style|The visual style of the text, see             Text.MakeStyle or the TextStyle object for more details.|
|RETURNS: Vec2|The width and height of the text in meters.|
```csharp
static Vec2 Size(string text)
```
Sometimes you just need to know how much room some text
takes up! This finds the size of the text in meters when using the
default style!

|  |  |
|--|--|
|string text|Text you want to find the size of.|
|RETURNS: Vec2|The width and height of the text in meters.|

# static class UI

This class is a collection of user interface and interaction
methods! StereoKit uses an Immediate Mode GUI system, which can be very
easy to work with and modify during runtime.

You must call the UI method every frame you wish it to be available,
and if you no longer want it to be present, you simply stop calling it!
The id of the element is used to track its state from frame to frame,
so for elements with state, you'll want to avoid changing the id during
runtime! Ids are also scoped per-window, so different windows can
re-use the same id, but a window cannot use the same id twice.

## UI.Settings

```csharp
static UISettings Settings{ set }
```

UI sizing and layout settings. Set only for now

## UI.ColorScheme

```csharp
static Color ColorScheme{ set }
```

StereoKit will generate a color palette from this gamma
space color, and use it to skin the UI!

## UI.ShowVolumes

```csharp
static bool ShowVolumes{ set }
```

Shows or hides the collision volumes of the UI! This is
for debug purposes, and can help identify visible and invisible
collision issues.

## UI.EnableFarInteract

```csharp
static bool EnableFarInteract{ get set }
```

Enables or disables the far ray grab interaction for
Handle elements like the Windows. It can be enabled and disabled
for individual UI elements, and if this remains disabled at the
start of the next frame, then the hand ray indicators will not be
visible. This is enabled by default.

## UI.LineHeight

```csharp
static float LineHeight{ get }
```

This is the height of a single line of text with padding in the UI's layout system!

## UI.AreaRemaining

```csharp
static Vec2 AreaRemaining{ get }
```

Use LayoutRemaining, removing in v0.4

## UI.LayoutRemaining

```csharp
static Vec2 LayoutRemaining{ get }
```

How much space is available on the current layout! This is
based on the current layout position, so X will give you the amount
remaining on the current line, and Y will give you distance to the
bottom of the layout, including the current line. These values will
be 0 if you're using 0 for the layout size on that axis.

## UI.LayoutAt

```csharp
static Vec3 LayoutAt{ get }
```

The hierarchy local position of the current UI layout
position. The top left point of the next UI element will be start
here!

## UI.LayoutLast

```csharp
static Bounds LayoutLast{ get }
```

These are the layout bounds of the most recently reserved
layout space. The Z axis dimensions are always 0. Only UI elements
that affect the surface's layout will report their bounds here. You
can reserve your own layout space via UI.LayoutReserve, and that
call will also report here.

## UI.LayoutReserve
```csharp
static Bounds LayoutReserve(Vec2 size, bool addPadding, float depth)
```
Reserves a box of space for an item in the current UI
layout! If either size axis is zero, it will be auto-sized to fill
the current surface horizontally, and fill a single LineHeight
vertically. Returns the Hierarchy local bounds of the space that
was reserved, with a Z axis dimension of 0.

|  |  |
|--|--|
|Vec2 size|Size of the layout box in Hierarchy local             meters.|
|bool addPadding|If true, this will add the current padding             value to the total final dimensions of the space that is reserved.|
|float depth|This allows you to quickly insert a depth into             the Bounds you're receiving. This will offset on the Z axis in             addition to increasing the dimensions, so that the bounds still             remain sitting on the surface of the UI.                          This depth value will not be reflected in the bounds provided by              LayouLast.|
|RETURNS: Bounds|Returns the Hierarchy local bounds of the space that was reserved, with a Z axis dimension of 0.|


## UI.LastElementHandUsed
```csharp
static BtnState LastElementHandUsed(Handed hand)
```
Tells if the hand was involved in the focus or active
state of the most recent UI element using an id.

|  |  |
|--|--|
|Handed hand|Which hand we're checking.|
|RETURNS: BtnState|A BtnState that indicated the hand was "just active" this frame, is currently "active" or if it "just became inactive" this frame.|


## UI.LastElementActive

```csharp
static BtnState LastElementActive{ get }
```

Tells the Active state of the most recent UI element that
used an id.

## UI.LastElementFocused

```csharp
static BtnState LastElementFocused{ get }
```

Tells the Focused state of the most recent UI element that
used an id.

## UI.IsInteracting
```csharp
static bool IsInteracting(Handed hand)
```
Tells if the user is currently interacting with a UI
element! This will be true if the hand has an active or focused UI
element.

|  |  |
|--|--|
|Handed hand|Which hand is interacting?|
|RETURNS: bool|True if the hand has an active or focused UI element. False otherwise.|


## UI.PushSurface
```csharp
static void PushSurface(Pose surfacePose, Vec3 layoutStart, Vec2 layoutDimensions)
```
This will push a surface into SK's UI layout system. The
surface becomes part of the transform hierarchy, and SK creates a
layout surface for UI content to be placed on and interacted with.
Must be accompanied by a PopSurface call.

|  |  |
|--|--|
|Pose surfacePose|The Pose of the UI surface, where the             surface forward direction is the same as the Pose's.|
|Vec3 layoutStart|This is an offset from the center of the             coordinate space created by the surfacePose. Vec3.Zero would mean             that content starts at the center of the surfacePose.|
|Vec2 layoutDimensions|The size of the surface area to use             during layout. Like other UI layout sizes, an axis set to zero             means it will auto-expand in that direction.|


## UI.PopSurface
```csharp
static void PopSurface()
```
This will return to the previous UI layout on the stack.
This must be called after a PushSurface call.


## UI.LayoutArea
```csharp
static void LayoutArea(Vec3 start, Vec2 dimensions)
```
Manually define what area is used for the UI layout. This
is in the current Hierarchy's coordinate space on the X/Y plane.

|  |  |
|--|--|
|Vec3 start|The top left of the layout area, relative to             the current Hierarchy in local meters.|
|Vec2 dimensions|The size of the layout area from the top             left, in local meters.|


## UI.ReserveBox
```csharp
static void ReserveBox(Vec2 size)
```
Use LayoutReserve, removing in v0.4


## UI.SameLine
```csharp
static void SameLine()
```
Moves the current layout position back to the end of the
line that just finished, so it can continue on the same line as the
last element!


## UI.NextLine
```csharp
static void NextLine()
```
This will advance the layout to the next line. If there's
nothing on the current line, it'll advance to the start of the next
on. But this won't have any affect on an empty line, try UI.Space
for that.


## UI.Space
```csharp
static void Space(float space)
```
Adds some space! If we're at the start of a new line,
space is added vertically, otherwise, space is added
horizontally.

|  |  |
|--|--|
|float space|Physical space to shift the layout by.|


## UI.VolumeAt
```csharp
static BtnState VolumeAt(string id, Bounds bounds, UIConfirm interactType, Handed& hand, BtnState& focusState)
```
A volume for helping to build one handed interactions.
This checks for the presence of a hand inside the bounds, and if
found, return that hand along with activation and focus
information defined by the interactType.

|  |  |
|--|--|
|string id|An id for tracking element state. MUST be unique             within current hierarchy.|
|Bounds bounds|Size and position of the volume, relative to             the current Hierarchy.|
|UIConfirm interactType|UIConfirm.Pinch will activate when the             hand performs a 'pinch' gesture. UIConfirm.Push will activate              when the hand enters the volume, and behave the same as element's             focusState.|
|Handed& hand|This will be the last unpreoccupied hand found             inside the volume, and is the hand controlling the interaction.|
|BtnState& focusState|The focus state tells if the element has             a hand inside of the volume that qualifies for focus.|
|RETURNS: BtnState|Based on the interactType, this is a BtnState that tells the activation state of the interaction.|
```csharp
static BtnState VolumeAt(string id, Bounds bounds, UIConfirm interactType, Handed& hand)
```
A volume for helping to build one handed interactions.
This checks for the presence of a hand inside the bounds, and if
found, return that hand along with activation and focus
information defined by the interactType.

|  |  |
|--|--|
|string id|An id for tracking element state. MUST be unique             within current hierarchy.|
|Bounds bounds|Size and position of the volume, relative to             the current Hierarchy.|
|UIConfirm interactType|UIConfirm.Pinch will activate when the             hand performs a 'pinch' gesture. UIConfirm.Push will activate              when the hand enters the volume, and behave the same as element's             focusState.|
|Handed& hand|This will be the last unpreoccupied hand found             inside the volume, and is the hand controlling the interaction.|
|RETURNS: BtnState|Based on the interactType, this is a BtnState that tells the activation state of the interaction.|
```csharp
static BtnState VolumeAt(string id, Bounds bounds, UIConfirm interactType)
```
A volume for helping to build one handed interactions.
This checks for the presence of a hand inside the bounds, and if
found, return that hand along with activation and focus
information defined by the interactType.

|  |  |
|--|--|
|string id|An id for tracking element state. MUST be unique             within current hierarchy.|
|Bounds bounds|Size and position of the volume, relative to             the current Hierarchy.|
|UIConfirm interactType|UIConfirm.Pinch will activate when the             hand performs a 'pinch' gesture. UIConfirm.Push will activate              when the hand enters the volume, and behave the same as element's             focusState.|
|RETURNS: BtnState|Based on the interactType, this is a BtnState that tells the activation state of the interaction.|


## UI.InteractVolume
```csharp
static BtnState InteractVolume(Bounds bounds, Handed& hand)
```
This method will be removed in v0.4, use UI.VolumeAt.

This watches a volume of space for pinch interaction
events! If a hand is inside the space indicated by the bounds,
this function will return that hand's pinch state, as well as
indicate which hand did it through the out parameter.

Note that since this only provides the hand's pinch state, it
won't give you JustActive and JustInactive notifications for
when the hand enters or leaves the volume.

|  |  |
|--|--|
|Bounds bounds|A UI hierarchy space bounding volume.|
|Handed& hand|This will be the last hand that provides a              pinch state within this volume. That means that if both hands are             pinching in this volume, it will provide the Right hand.|
|RETURNS: BtnState|This will be the pinch state of the last hand that provides a pinch state within this volume. That means that if both hands are pinching in this volume, it will provide the pinch state of the Right hand.|


## UI.HSeparator
```csharp
static void HSeparator()
```
This draws a line horizontally across the current
layout. Makes a good separator between sections of UI!


## UI.Label
```csharp
static void Label(string text, bool usePadding)
```
Adds some text to the layout! Text uses the UI's current
font settings, which can be changed with UI.Push/PopTextStyle. Can
contain newlines!

|  |  |
|--|--|
|string text|Label text to display. Can contain newlines!             Doesn't use text as id, so it can be non-unique.|
|bool usePadding|Should padding be included for             positioning this text? Sometimes you just want un-padded text!|
```csharp
static void Label(string text, Vec2 size)
```
Adds some text to the layout, but this overload allows you
can specify the size that you want it to use. Text uses the UI's
current font settings, which can be changed with
UI.Push/PopTextStyle. Can contain newlines!

|  |  |
|--|--|
|string text|Label text to display. Can contain newlines!             Doesn't use text as id, so it can be non-unique.|
|Vec2 size|The layout size for this element in Hierarchy             space. If an axis is left as zero, it will be auto-calculated. For             X this is the remaining width of the current layout, and for Y this             is UI.LineHeight.|


## UI.Text
```csharp
static void Text(string text, TextAlign textAlign)
```
Displays a large chunk of text on the current layout.
This can include new lines and spaces, and will properly wrap
once it fills the entire layout! Text uses the UI's current font
settings, which can be changed with UI.Push/PopTextStyle.

|  |  |
|--|--|
|string text|The text you wish to display, there's no              additional parsing done to this text, so put it in as you want to             see it!|
|TextAlign textAlign|Where should the text position itself             within its bounds? TextAlign.TopLeft is how most English text is             aligned.|


## UI.Image
```csharp
static void Image(Sprite image, Vec2 size)
```
Adds an image to the UI!

|  |  |
|--|--|
|Sprite image|A valid sprite.|
|Vec2 size|Size in Hierarchy local meters. If one of the             components is 0, it'll be automatically determined from the other             component and the image's aspect ratio.|


## UI.Button
```csharp
static bool Button(string text)
```
A pressable button! A button will expand to fit the text
provided to it, vertically and horizontally. Text is re-used as the
id. Will return true only on the first frame it is pressed!

|  |  |
|--|--|
|string text|Text to display on the button and id for             tracking element state. MUST be unique within current hierarchy.|
|RETURNS: bool|Will return true only on the first frame it is pressed!|
```csharp
static bool Button(string text, Vec2 size)
```
A pressable button! A button will expand to fit the text
provided to it, vertically and horizontally. Text is re-used as the
id. Will return true only on the first frame it is pressed!

|  |  |
|--|--|
|string text|Text to display on the button and id for             tracking element state. MUST be unique within current hierarchy.|
|Vec2 size|The layout size for this element in Hierarchy             space. If an axis is left as zero, it will be auto-calculated. For             X this is the remaining width of the current layout, and for Y this             is UI.LineHeight.|
|RETURNS: bool|Will return true only on the first frame it is pressed!|


## UI.ButtonAt
```csharp
static bool ButtonAt(string text, Vec3 topLeftCorner, Vec2 size)
```
A variant of UI.Button that doesn't use the layout system,
and instead goes exactly where you put it.

|  |  |
|--|--|
|string text|Text to display on the button and id for             tracking element state. MUST be unique within current hierarchy.|
|Vec3 topLeftCorner|This is the top left corner of the UI             element relative to the current Hierarchy.|
|Vec2 size|The layout size for this element in Hierarchy             space. If an axis is left as zero, it will be auto-calculated. For             X this is the remaining width of the current layout, and for Y this             is UI.LineHeight.|
|RETURNS: bool|Will return true only on the first frame it is pressed!|


## UI.ButtonImg
```csharp
static bool ButtonImg(string text, Sprite image, UIBtnLayout imageLayout)
```
A pressable button accompanied by an image! The button
will expand to fit the text provided to it, horizontally. Text is
re-used as the id. Will return true only on the first frame it is
pressed!

|  |  |
|--|--|
|string text|Text to display on the button and id for             tracking element state. MUST be unique within current hierarchy.|
|Sprite image|This is the image that will be drawn along with             the text. See imageLayout for where the image gets drawn!|
|UIBtnLayout imageLayout|This enum specifies how the text and             image should be laid out on the button. For example, `UIBtnLayout.Left`             will have the image on the left, and text on the right.|
|RETURNS: bool|Will return true only on the first frame it is pressed!|
```csharp
static bool ButtonImg(string text, Sprite image, UIBtnLayout imageLayout, Vec2 size)
```
A pressable button accompanied by an image! The button
will expand to fit the text provided to it, horizontally. Text is
re-used as the id. Will return true only on the first frame it is
pressed!

|  |  |
|--|--|
|string text|Text to display on the button and id for             tracking element state. MUST be unique within current hierarchy.|
|Sprite image|This is the image that will be drawn along with             the text. See imageLayout for where the image gets drawn!|
|UIBtnLayout imageLayout|This enum specifies how the text and             image should be laid out on the button. For example, `UIBtnLayout.Left`             will have the image on the left, and text on the right.|
|Vec2 size|The layout size for this element in Hierarchy             space. If an axis is left as zero, it will be auto-calculated. For             X this is the remaining width of the current layout, and for Y this             is UI.LineHeight.|
|RETURNS: bool|Will return true only on the first frame it is pressed!|


## UI.ButtonImgAt
```csharp
static bool ButtonImgAt(string text, Sprite image, UIBtnLayout imageLayout, Vec3 topLeftCorner, Vec2 size)
```
A variant of UI.ButtonImg that doesn't use the layout
system, and instead goes exactly where you put it.

|  |  |
|--|--|
|string text|Text to display on the button and id for             tracking element state. MUST be unique within current hierarchy.|
|Sprite image|This is the image that will be drawn along with             the text. See imageLayout for where the image gets drawn!|
|UIBtnLayout imageLayout|This enum specifies how the text and             image should be laid out on the button. For example, `UIBtnLayout.Left`             will have the image on the left, and text on the right.|
|Vec3 topLeftCorner|This is the top left corner of the UI             element relative to the current Hierarchy.|
|Vec2 size|The layout size for this element in Hierarchy             space. If an axis is left as zero, it will be auto-calculated. For             X this is the remaining width of the current layout, and for Y this             is UI.LineHeight.|
|RETURNS: bool|Will return true only on the first frame it is pressed!|


## UI.Radio
```csharp
static bool Radio(string text, bool active)
```
A Radio is similar to a button, except you can specify if
it looks pressed or not regardless of interaction. This can be
useful for radio-like behavior! Check an enum for a value, and use
that as the 'active' state, Then switch to that enum value if Radio
returns true.

|  |  |
|--|--|
|string text|Text to display on the Radio and id for             tracking element state. MUST be unique within current hierarchy.|
|bool active|Does this button look like it's pressed?|
|RETURNS: bool|Will return true only on the first frame it is pressed!|
```csharp
static bool Radio(string text, bool active, Vec2 size)
```
A Radio is similar to a button, except you can specify if
it looks pressed or not regardless of interaction. This can be
useful for radio-like behavior! Check an enum for a value, and use
that as the 'active' state, Then switch to that enum value if Radio
returns true.

|  |  |
|--|--|
|string text|Text to display on the Radio and id for             tracking element state. MUST be unique within current hierarchy.|
|bool active|Does this button look like it's pressed?|
|Vec2 size|The layout size for this element in Hierarchy             space. If an axis is left as zero, it will be auto-calculated. For             X this is the remaining width of the current layout, and for Y this             is UI.LineHeight.|
|RETURNS: bool|Will return true only on the first frame it is pressed!|


## UI.ButtonRound
```csharp
static bool ButtonRound(string id, Sprite image, float diameter)
```
A pressable button! A button will expand to fit the text
provided to it, vertically and horizontally. Text is re-used as the
id. Will return true only on the first frame it is pressed!

|  |  |
|--|--|
|string id|An id for tracking element state. MUST be unique             within current hierarchy.|
|Sprite image|An image to display as the face of the button.|
|float diameter|The diameter of the button's visual.|
|RETURNS: bool|Will return true only on the first frame it is pressed!|


## UI.ButtonRoundAt
```csharp
static bool ButtonRoundAt(string id, Sprite image, Vec3 topLeftCorner, float diameter)
```
A variant of UI.ButtonRound that doesn't use the layout
system, and instead goes exactly where you put it.

|  |  |
|--|--|
|string id|An id for tracking element state. MUST be unique             within current hierarchy.|
|Sprite image|An image to display as the face of the button.|
|Vec3 topLeftCorner|This is the top left corner of the UI             element relative to the current Hierarchy.|
|float diameter|The diameter of the button's visual.|
|RETURNS: bool|Will return true only on the first frame it is pressed!|


## UI.Toggle
```csharp
static bool Toggle(string text, Boolean& value)
```
A toggleable button! A button will expand to fit the
text provided to it, vertically and horizontally. Text is re-used
as the id. Will return true any time the toggle value changes, NOT
the toggle value itself!

|  |  |
|--|--|
|string text|Text to display on the Toggle and id for             tracking element state. MUST be unique within current hierarchy.|
|Boolean& value|The current state of the toggle button! True              means it's toggled on, and false means it's toggled off.|
|RETURNS: bool|Will return true any time the toggle value changes, NOT the toggle value itself!|
```csharp
static bool Toggle(string text, Boolean& value, Vec2 size)
```
A toggleable button! A button will expand to fit the
text provided to it, vertically and horizontally. Text is re-used
as the id. Will return true any time the toggle value changes, NOT
the toggle value itself!

|  |  |
|--|--|
|string text|Text to display on the Toggle and id for             tracking element state. MUST be unique within current hierarchy.|
|Boolean& value|The current state of the toggle button! True              means it's toggled on, and false means it's toggled off.|
|Vec2 size|The layout size for this element in Hierarchy             space. If an axis is left as zero, it will be auto-calculated. For             X this is the remaining width of the current layout, and for Y this             is UI.LineHeight.|
|RETURNS: bool|Will return true any time the toggle value changes, NOT the toggle value itself!|


## UI.ToggleAt
```csharp
static bool ToggleAt(string text, Boolean& value, Vec3 topLeftCorner, Vec2 size)
```
A variant of UI.Toggle that doesn't use the layout system,
and instead goes exactly where you put it.

|  |  |
|--|--|
|string text|Text to display on the Toggle and id for             tracking element state. MUST be unique within current hierarchy.|
|Vec3 topLeftCorner|This is the top left corner of the UI             element relative to the current Hierarchy.|
|Vec2 size|The layout size for this element in Hierarchy             space. If an axis is left as zero, it will be auto-calculated. For             X this is the remaining width of the current layout, and for Y this             is UI.LineHeight.|
|RETURNS: bool|Will return true any time the toggle value changes, NOT the toggle value itself!|


## UI.Input
```csharp
static bool Input(string id, String& value, Vec2 size, TextContext type)
```
This is an input field where users can input text to the
app! Selecting it will spawn a virtual keyboard, or act as the
keyboard focus. Hitting escape or enter, or focusing another UI
element will remove focus from this Input.

|  |  |
|--|--|
|string id|An id for tracking element state. MUST be unique             within current hierarchy.|
|String& value|The string that will store the Input's              content in.|
|Vec2 size|Size of the Input in Hierarchy local meters.             Zero axes will auto-size.|
|TextContext type|What category of text this Input represents.             This may affect what kind of soft keyboard will be displayed, if             one is shown to the user.|
|RETURNS: bool|Returns true every time the contents of 'value' change.|


## UI.ProgressBar
```csharp
static void ProgressBar(float percent, float width)
```
This is a simple horizontal progress indicator bar. This
is used by the HSlider to draw the slider bar beneath the
interactive element. Does not include any text or label.

|  |  |
|--|--|
|float percent|A value between 0 and 1 indicating progress             from 0% to 100%.|
|float width|Physical width of the slider on the window. 0             will fill the remaining amount of window space.|


## UI.ProgressBarAt
```csharp
static void ProgressBarAt(float percent, Vec3 topLeftCorner, Vec2 size)
```
This is a simple horizontal progress indicator bar. This
is used by the HSlider to draw the slider bar beneath the
interactive element. Does not include any text or label.

|  |  |
|--|--|
|float percent|A value between 0 and 1 indicating progress             from 0% to 100%.|
|Vec3 topLeftCorner|This is the top left corner of the UI             element relative to the current Hierarchy.|
|Vec2 size|The layout size for this element in Hierarchy             space. If an axis is left as zero, it will be auto-calculated. For             X this is the remaining width of the current layout, and for Y this             is UI.LineHeight.|


## UI.HSlider
```csharp
static bool HSlider(string id, Single& value, float min, float max, float step, float width, UIConfirm confirmMethod)
```
A horizontal slider element! You can stick your finger
in it, and slide the value up and down.

|  |  |
|--|--|
|string id|An id for tracking element state. MUST be unique             within current hierarchy.|
|Single& value|The value that the slider will store slider              state in.|
|float min|The minimum value the slider can set, left side              of the slider.|
|float max|The maximum value the slider can set, right              side of the slider.|
|float step|Locks the value to intervals of step. Starts              at min, and increments by step.|
|float width|Physical width of the slider on the window. 0             will fill the remaining amount of window space.|
|UIConfirm confirmMethod|How should the slider be activated?             Push will be a push-button the user must press first, and pinch             will be a tab that the user must pinch and drag around.|
|RETURNS: bool|Returns true any time the value changes.|
```csharp
static bool HSlider(string id, Double& value, double min, double max, double step, float width, UIConfirm confirmMethod)
```
A horizontal slider element! You can stick your finger
in it, and slide the value up and down.

|  |  |
|--|--|
|string id|An id for tracking element state. MUST be unique             within current hierarchy.|
|Double& value|The value that the slider will store slider              state in.|
|double min|The minimum value the slider can set, left side              of the slider.|
|double max|The maximum value the slider can set, right              side of the slider.|
|double step|Locks the value to intervals of step. Starts              at min, and increments by step.|
|float width|Physical width of the slider on the window. 0             will fill the remaining amount of window space.|
|UIConfirm confirmMethod|How should the slider be activated?             Push will be a push-button the user must press first, and pinch             will be a tab that the user must pinch and drag around.|
|RETURNS: bool|Returns true any time the value changes.|


## UI.HSliderAt
```csharp
static bool HSliderAt(string id, Single& value, float min, float max, float step, Vec3 topLeftCorner, Vec2 size, UIConfirm confirmMethod)
```
A variant of UI.HSlider that doesn't use the layout
system, and instead goes exactly where you put it.

|  |  |
|--|--|
|string id|An id for tracking element state. MUST be unique             within current hierarchy.|
|Single& value|The value that the slider will store slider              state in.|
|float min|The minimum value the slider can set, left side              of the slider.|
|float max|The maximum value the slider can set, right              side of the slider.|
|float step|Locks the value to intervals of step. Starts              at min, and increments by step.|
|Vec3 topLeftCorner|This is the top left corner of the UI             element relative to the current Hierarchy.|
|Vec2 size|The layout size for this element in Hierarchy             space. If an axis is left as zero, it will be auto-calculated. For             X this is the remaining width of the current layout, and for Y this             is UI.LineHeight.|
|UIConfirm confirmMethod|How should the slider be activated?             Push will be a push-button the user must press first, and pinch             will be a tab that the user must pinch and drag around.|
|RETURNS: bool|Returns true any time the value changes.|
```csharp
static bool HSliderAt(string id, Double& value, double min, double max, double step, Vec3 topLeftCorner, Vec2 size, UIConfirm confirmMethod)
```
A variant of UI.HSlider that doesn't use the layout
system, and instead goes exactly where you put it.

|  |  |
|--|--|
|string id|An id for tracking element state. MUST be unique             within current hierarchy.|
|Double& value|The value that the slider will store slider              state in.|
|double min|The minimum value the slider can set, left side              of the slider.|
|double max|The maximum value the slider can set, right              side of the slider.|
|double step|Locks the value to intervals of step. Starts              at min, and increments by step.|
|Vec3 topLeftCorner|This is the top left corner of the UI             element relative to the current Hierarchy.|
|Vec2 size|The layout size for this element in Hierarchy             space. If an axis is left as zero, it will be auto-calculated. For             X this is the remaining width of the current layout, and for Y this             is UI.LineHeight.|
|UIConfirm confirmMethod|How should the slider be activated?             Push will be a push-button the user must press first, and pinch             will be a tab that the user must pinch and drag around.|
|RETURNS: bool|Returns true any time the value changes.|


## UI.HandleBegin
```csharp
static bool HandleBegin(string id, Pose& pose, Bounds handle, bool drawHandle, UIMove moveType)
```
This begins a new UI group with its own layout! Much
like a window, except with a more flexible handle, and no header.
You can draw the handle, but it will have no text on it.
The pose value is always relative to the current hierarchy stack.
This call will also push the pose transform onto the hierarchy stack, so
any objects drawn up to the corresponding UI.HandleEnd() will get transformed
by the handle pose. Returns true for every frame the user is grabbing the handle.

|  |  |
|--|--|
|string id|An id for tracking element state. MUST be unique             within current hierarchy.|
|Pose& pose|The pose state for the handle! The user will              be able to grab this handle and move it around. The pose is relative             to the current hierarchy stack.|
|Bounds handle|Size and location of the handle, relative to              the pose.|
|bool drawHandle|Should this function draw the handle              visual for you, or will you draw that yourself?|
|UIMove moveType|Describes how the handle will move when              dragged around.|
|RETURNS: bool|Returns true for every frame the user is grabbing the handle.|


## UI.HandleEnd
```csharp
static void HandleEnd()
```
Finishes a handle! Must be called after UI.HandleBegin()
and all elements have been drawn. Pops the pose transform pushed
by UI.HandleBegin() from the hierarchy stack.


## UI.Handle
```csharp
static bool Handle(string id, Pose& pose, Bounds handle, bool drawHandle, UIMove moveType)
```
This begins and ends a handle so you can just use  its
grabbable/moveable functionality! Behaves much like a window,
except with a more flexible handle, and no header. You can draw
the handle, but it will have no text on it. Returns true for
every frame the user is grabbing the handle.

|  |  |
|--|--|
|string id|An id for tracking element state. MUST be unique             within current hierarchy.|
|Pose& pose|The pose state for the handle! The user will              be able to grab this handle and move it around. The pose is relative             to the current hierarchy stack.|
|Bounds handle|Size and location of the handle, relative to              the pose.|
|bool drawHandle|Should this function draw the handle for              you, or will you draw that yourself?|
|UIMove moveType|Describes how the handle will move when              dragged around.|
|RETURNS: bool|Returns true for every frame the user is grabbing the handle.|


## UI.WindowBegin
```csharp
static void WindowBegin(string text, Pose& pose, Vec2 size, UIWin windowType, UIMove moveType)
```
Begins a new window! This will push a pose onto the
transform stack, and all UI elements will be relative to that new
pose. The pose is actually the top-center of the window. Must be
finished with a call to UI.WindowEnd().

|  |  |
|--|--|
|string text|Text to display on the window title and id for             tracking element state. MUST be unique within current hierarchy.|
|Pose& pose|The pose state for the window! If showHeader              is true, the user will be able to grab this header and move it              around.|
|Vec2 size|Physical size of the window! If either              dimension is 0, then the size on that axis will be auto-             calculated based on the content provided during the previous              frame.|
|UIWin windowType|Describes how the window should be drawn,             use a header, a body, neither, or both?|
|UIMove moveType|Describes how the window will move when              dragged around.|
```csharp
static void WindowBegin(string text, Pose& pose, UIWin windowType, UIMove moveType)
```
Begins a new window! This will push a pose onto the
transform stack, and all UI elements will be relative to that new
pose. The pose is actually the top-center of the window. Must be
finished with a call to UI.WindowEnd(). This override omits the
size value, so the size will be auto-calculated based on the
content provided during the previous frame.

|  |  |
|--|--|
|string text|Text to display on the window title and id for             tracking element state. MUST be unique within current hierarchy.|
|Pose& pose|The pose state for the window! If showHeader              is true, the user will be able to grab this header and move it              around.|
|UIWin windowType|Describes how the window should be drawn,             use a header, a body, neither, or both?|
|UIMove moveType|Describes how the window will move when              dragged around.|


## UI.WindowEnd
```csharp
static void WindowEnd()
```
Finishes a window! Must be called after UI.WindowBegin()
and all elements have been drawn.


## UI.PushId
```csharp
static void PushId(string rootId)
```
Adds a root id to the stack for the following UI
elements! This id is combined when hashing any following ids, to
prevent id collisions in separate groups.

|  |  |
|--|--|
|string rootId|The root id to use until the following PopId              call. MUST be unique within current hierarchy.|
```csharp
static void PushId(int rootId)
```
Adds a root id to the stack for the following UI
elements! This id is combined when hashing any following ids, to
prevent id collisions in separate groups.

|  |  |
|--|--|
|int rootId|The root id to use until the following PopId              call. MUST be unique within current hierarchy.|


## UI.PopId
```csharp
static void PopId()
```
Removes the last root id from the stack, and moves up to
the one before it!


## UI.PushPreserveKeyboard
```csharp
static void PushPreserveKeyboard(bool preserveKeyboard)
```
When a soft keyboard is visible, interacting with UI
elements will cause the keyboard to close. This function allows you
to change this behavior for certain UI elements, allowing the user
to interact and still preserve the keyboard's presence. Remember
to Pop when you're finished!

|  |  |
|--|--|
|bool preserveKeyboard|If true, interacting with elements             will NOT hide the keyboard. If false, interaction will hide the             keyboard.|


## UI.PopPreserveKeyboard
```csharp
static void PopPreserveKeyboard()
```
This pops the keyboard presentation state to what it was
previously.


## UI.PushTextStyle
```csharp
static void PushTextStyle(TextStyle style)
```
This pushes a Text Style onto the style stack! All text
elements rendered by the GUI system will now use this styling.

|  |  |
|--|--|
|TextStyle style|A valid TextStyle to use.|


## UI.PopTextStyle
```csharp
static void PopTextStyle()
```
Removes a TextStyle from the stack, and whatever was
below will then be used as the GUI's primary font.


## UI.PushTint
```csharp
static void PushTint(Color colorGamma)
```
All UI between PushTint and its matching PopTint will be
tinted with this color. This is implemented by multiplying this
color with the current color of the UI element. The default is a
White (1,1,1,1) identity tint.

|  |  |
|--|--|
|Color colorGamma|A normal (gamma corrected) color value.             This is internally converted to linear, so tint multiplication             happens on linear color values.|


## UI.PopTint
```csharp
static void PopTint()
```
Removes a Tint from the stack, and whatever was below will
then be used as the primary tint.


## UI.PushEnabled
```csharp
static void PushEnabled(bool enabled)
```
All UI between PushEnabled and its matching PopEnabled
will set the UI to an enabled or disabled state, allowing or
preventing interaction with specific elements. The default state is
true. This currently doesn't have any visual effect, so you may
wish to pair it with a PushTint.

|  |  |
|--|--|
|bool enabled|Should the following elements be enabled and             interactable?|


## UI.PopEnabled
```csharp
static void PopEnabled()
```
Removes an 'enabled' state from the stack, and whatever
was below will then be used as the primary enabled state.


## UI.PanelAt
```csharp
static void PanelAt(Vec3 start, Vec2 size, UIPad padding)
```
If you wish to manually draw a Panel, this function will
let you draw one wherever you want!

|  |  |
|--|--|
|Vec3 start|The top left corner of the Panel element.|
|Vec2 size|The size of the Panel element, in hierarchy             local meters.|
|UIPad padding|Only UIPad.Outsize has any affect here.             UIPad.Inside will behave the same as UIPad.None.|


## UI.PanelBegin
```csharp
static void PanelBegin(UIPad padding)
```
This will begin a Panel element that will encompass all
elements drawn between PanelBegin and PanelEnd. This is an entirely
visual element, and is great for visually grouping elements
together. Every Begin must have a matching End.

|  |  |
|--|--|
|UIPad padding|Describes how padding is applied to the             visual element of the Panel.|


## UI.PanelEnd
```csharp
static void PanelEnd()
```
This will finalize and draw a Panel element.


## UI.SetElementVisual
```csharp
static void SetElementVisual(UIVisual visual, Mesh mesh, Material material, Vec2 minSize)
```
Override the visual assets attached to a particular UI
element.

Note that StereoKit's default UI assets use a type of quadrant
sizing that is implemented in the Material _and_ the Mesh. You
don't need to use quadrant sizing for your own visuals, but if
you wish to know more, you can read more about the technique
[here](https://playdeck.net/blog/quadrant-sizing-efficient-ui-rendering).
You may also find UI.QuadrantSizeVerts and UI.QuadrantSizeMesh to
be helpful.

|  |  |
|--|--|
|UIVisual visual|Which UI visual element to override.|
|Mesh mesh|The Mesh to use for the UI element's visual             component. The Mesh will be scaled to match the dimensions of the             UI element.|
|Material material|The Material to use when rendering the UI             element. The default Material is specifically designed to work             with quadrant sizing formatted meshes.|
|Vec2 minSize|For some meshes, such as quadrant sized             meshes, there's a minimum size where the mesh turns inside out.             This lets UI elements to accommodate for this minimum size, and             behave somewhat more appropriately.|


## UI.QuadrantSizeVerts
```csharp
static void QuadrantSizeVerts(Vertex[] verts, float overflowPercent)
```
This will reposition the vertices to work well with
quadrant resizing shaders. The mesh should generally be centered
around the origin, and face down the -Z axis. This will also
overwrite any UV coordinates in the verts.

You can read more about the technique [here](https://playdeck.net/blog/quadrant-sizing-efficient-ui-rendering).

|  |  |
|--|--|
|Vertex[] verts|A list of vertices to be modified to fit the             sizing shader.|
|float overflowPercent|When scaled, should the geometry             stick out past the "box" represented by the scale, or edge up             against it? A value of 0 will mean the geometry will fit entirely             inside the "box", and a value of 1 means the geometry will start at             the boundary of the box and continue outside it.|


## UI.QuadrantSizeMesh
```csharp
static void QuadrantSizeMesh(Mesh& mesh, float overflowPercent)
```
This will reposition the Mesh's vertices to work well with
quadrant resizing shaders. The mesh should generally be centered
around the origin, and face down the -Z axis. This will also
overwrite any UV coordinates in the verts.

You can read more about the technique [here](https://playdeck.net/blog/quadrant-sizing-efficient-ui-rendering).

|  |  |
|--|--|
|Mesh& mesh|The vertices of this Mesh will be retrieved,             modified, and overwritten.|
|float overflowPercent|When scaled, should the geometry             stick out past the "box" represented by the scale, or edge up             against it? A value of 0 will mean the geometry will fit entirely             inside the "box", and a value of 1 means the geometry will start at             the boundary of the box and continue outside it.|

# static class World

World contains information about the real world around the
user. This includes things like play boundaries, scene understanding,
and other various things.

## World.HasBounds

```csharp
static bool HasBounds{ get }
```

This refers to the play boundary, or guardian system
that the system may have! Not all systems have this, so it's
always a good idea to check this first!

## World.BoundsSize

```csharp
static Vec2 BoundsSize{ get }
```

This is the size of a rectangle within the play
boundary/guardian's space, in meters if one exists. Check
`World.BoundsPose` for the center point and orientation of the
boundary, and check `World.HasBounds` to see if it exists at all!

## World.BoundsPose

```csharp
static Pose BoundsPose{ get }
```

This is the orientation and center point of the system's
boundary/guardian. This can be useful to find the floor height!
Not all systems have a boundary, so be sure to check
`World.HasBounds` first.

## World.RefreshType

```csharp
static WorldRefresh RefreshType{ get set }
```

What information should StereoKit use to determine when
the next world data refresh happens? See the `WorldRefresh` enum
for details.

## World.RefreshRadius

```csharp
static float RefreshRadius{ get set }
```

Radius, in meters, of the area that StereoKit should
scan for world data. Default is 4. When using the
`WorldRefresh.Area` refresh type, the world data will refresh
when the user has traveled half this radius from the center of
where the most recent refresh occurred.

## World.RefreshInterval

```csharp
static float RefreshInterval{ get set }
```

The refresh interval speed, in seconds. This is only
applicable when using `WorldRefresh.Timer` for the refresh type.
Note that the system may not be able to refresh as fast as you
wish, and in that case, StereoKit will always refresh as soon as
the previous refresh finishes.

## World.FromSpatialNode
```csharp
static Pose FromSpatialNode(Guid spatialNodeGuid, SpatialNodeType spatialNodeType, Int64 qpcTime)
```
Converts a Windows Mirage spatial node GUID into a Pose
based on its current position and rotation! Check
SK.System.spatialBridgePresent to see if this is available to
use. Currently only on HoloLens, good for use with the Windows
QR code package.

|  |  |
|--|--|
|Guid spatialNodeGuid|A Windows Mirage spatial node GUID             acquired from a windows MR API call.|
|SpatialNodeType spatialNodeType|Type of spatial node to locate.|
|Int64 qpcTime|A windows performance counter timestamp at             which the node should be located, obtained from another API or             with System.Diagnostics.Stopwatch.GetTimestamp().|
|RETURNS: Pose|A Pose representing the current orientation of the spatial node.|


## World.FromPerceptionAnchor
```csharp
static Pose FromPerceptionAnchor(Object perceptionSpatialAnchor)
```
Converts a Windows.Perception.Spatial.SpatialAnchor's pose
into SteroKit's coordinate system. This can be great for
interacting with some of the UWP spatial APIs such as WorldAnchors.

This method only works on UWP platforms, check
SK.System.perceptionBridgePresent to see if this is available.

|  |  |
|--|--|
|Object perceptionSpatialAnchor|A valid             Windows.Perception.Spatial.SpatialAnchor.|
|RETURNS: Pose|A Pose representing the current orientation of the SpatialAnchor.|


## World.Raycast
```csharp
static bool Raycast(Ray ray, Ray& intersection)
```
World.RaycastEnabled must be set to true first!
SK.System.worldRaycastPresent must also be true. This does a ray
intersection with whatever represents the environment at the
moment! In this case, it's a watertight collection of low
resolution meshes calculated by the Scene Understanding
extension, which is only provided by the Microsoft HoloLens
runtime.

|  |  |
|--|--|
|Ray ray|A world space ray that you'd like to try             intersecting with the world mesh.|
|Ray& intersection|The location of the intersection, and             direction of the world's surface at that point. This is only             valid if the method returns true.|
|RETURNS: bool|True if an intersection is detected, false if raycasting is disabled, or there was no intersection.|


## World.OcclusionEnabled

```csharp
static bool OcclusionEnabled{ get set }
```

Off by default. This tells StereoKit to load up and
display an occlusion surface that allows the real world to
occlude the application's digital content! Most systems may allow
you to customize the visual appearance of this occlusion surface
via the World.OcclusionMaterial.
Check SK.System.worldOcclusionPresent to see if occlusion can be
enabled. This will reset itself to false if occlusion isn't
possible. Loading occlusion data is asynchronous, so occlusion
may not occur immediately after setting this flag.

## World.RaycastEnabled

```csharp
static bool RaycastEnabled{ get set }
```

Off by default. This tells StereoKit to load up
collision meshes for the environment, for use with World.Raycast.
Check SK.System.worldRaycastPresent to see if raycasting can be
enabled. This will reset itself to false if raycasting isn't
possible. Loading raycasting data is asynchronous, so collision
surfaces may not be available immediately after setting this
flag.

## World.OcclusionMaterial

```csharp
static Material OcclusionMaterial{ get set }
```

By default, this is a black(0,0,0,0) opaque unlit
material that will occlude geometry, but won't show up as visible
anywhere. You can override this with whatever material you would
like.
# struct Color32

A 32 bit color struct! This is often directly used by StereoKit data
structures, and so is often necessary for setting texture data, or mesh data.
Note that the Color type implicitly converts to Color32, so you can use the
static methods there to create Color32 values!

## Color32.r

```csharp
Byte r
```

Color components in the range of 0-255.

## Color32.g

```csharp
Byte g
```

Color components in the range of 0-255.

## Color32.b

```csharp
Byte b
```

Color components in the range of 0-255.

## Color32.a

```csharp
Byte a
```

Color components in the range of 0-255.

## Color32.White

```csharp
static Color32 White
```

Pure opaque white! Same as (255,255,255,255).

## Color32.Black

```csharp
static Color32 Black
```

Pure opaque black! Same as (0,0,0,255).

## Color32.BlackTransparent

```csharp
static Color32 BlackTransparent
```

Pure transparent black! Same as (0,0,0,0).

## Color32.Hex
```csharp
static Color32 Hex(uint hexValue)
```
Create a color from an integer based hex value! This can
make it easier to copy over colors from the web. This isn't a
string function though, so you'll need to fill it out the whole
way. Ex: `Color.Hex(0x0000FFFF)` would be RGBA(0,0,255,255).

|  |  |
|--|--|
|uint hexValue|An integer representing RGBA hex values!             Like: `0x0000FFFF`.|
|RETURNS: Color32|A 32 bit Color32 value.|


## Color32.ToString
```csharp
string ToString()
```
Mostly for debug purposes, this is a decent way to log or
inspect the color in debug mode. Looks like "[r, g, b, a]"

|  |  |
|--|--|
|RETURNS: string|A string that looks like "[r, g, b, a]"|

# struct Color

A color value stored as 4 floats with values that are
generally between 0 and 1! Note that there's also a Color32
structure, and that 4 floats is generally a lot more than you need.
So, use this for calculating individual colors at quality, but maybe
store them en-masse with Color32!

Also note that RGB is often a terrible color format for picking
colors, but it's how our displays work and we're stuck with it. If
you want to create a color via code, try out the static Color.HSV
method instead!

A note on gamma space vs. linear space colors! `Color` is not
inherently one or the other, but most color values we work with tend
to be gamma space colors, so most functions in StereoKit are gamma
space. There are occasional functions that will ask for linear space
colors instead, primarily in performance critical places, or places
where a color may not always be a color! However, performing math on
gamma space colors is bad, and will result in incorrect colors. We do
our best to indicate what color space a function uses, but it's not
enforced through syntax!

## Color.White

```csharp
static Color White
```

Pure opaque white! Same as (1,1,1,1).

## Color.Black

```csharp
static Color Black
```

Pure opaque black! Same as (0,0,0,1).

## Color.BlackTransparent

```csharp
static Color BlackTransparent
```

Pure transparent black! Same as (0,0,0,0).

## Color.r

```csharp
float r
```

Red component, a value that is generally between 0-1

## Color.g

```csharp
float g
```

Green component, a value that is generally between 0-1

## Color.b

```csharp
float b
```

Blue component, a value that is generally between 0-1

## Color.a

```csharp
float a
```

Alpha, or opacity component, a value that is generally between 0-1, where 0 is
completely transparent, and 1 is completely opaque.

## Color.Color
```csharp
void Color(float red, float green, float blue, float opacity)
```
Try Color.HSV instead! But if you really need to create a color from RGB
values, I suppose you're in the right place. All parameter values are generally in
the range of 0-1.

|  |  |
|--|--|
|float red|Red component, 0-1.|
|float green|Green component, 0-1.|
|float blue|Blue component, 0-1.|
|float opacity|Opacity, or the alpha component, 0-1 where 0 is completely              transparent, and 1 is completely opaque.|


## Color.ToHSV
```csharp
Vec3 ToHSV()
```
Converts the gamma space color to a Hue/Saturation/Value
format! Does not consider transparency when calculating the
result.

|  |  |
|--|--|
|RETURNS: Vec3|Hue, Saturation, and Value, stored in x, y, and z respectively. All values are between 0-1.|


## Color.ToLAB
```csharp
Vec3 ToLAB()
```
Converts the gamma space RGB color to a CIE LAB color
space value! Conversion back and forth from LAB space could be
somewhat lossy.

|  |  |
|--|--|
|RETURNS: Vec3|An LAB vector where x=L, y=A, z=B.|


## Color.HSV
```csharp
static Color HSV(float hue, float saturation, float value, float opacity)
```
Creates a Red/Green/Blue gamma space color from
Hue/Saturation/Value information.

|  |  |
|--|--|
|float hue|Hue most directly relates to the color as we             think of it! 0 is red, 0.1667 is yellow, 0.3333 is green, 0.5 is             cyan, 0.6667 is blue, 0.8333 is magenta, and 1 is red again!|
|float saturation|The vibrancy of the color, where 0 is             straight up a shade of gray, and 1 is 'poke you in the eye             colorful'.|
|float value|The brightness of the color! 0 is always             black.|
|float opacity|Also known as alpha! This is does not             affect the rgb components of the resulting color, it'll just get             slotted into the colors opacity value.|
|RETURNS: Color|A gamma space RGB color!|
```csharp
static Color HSV(Vec3 hsvColor, float opacity)
```
Creates a Red/Green/Blue gamma space color from
Hue/Saturation/Value information.

|  |  |
|--|--|
|Vec3 hsvColor|For convenience, XYZ is HSV.                          Hue most directly relates to the color as we think of it! 0 is             red, 0.1667 is yellow, 0.3333 is green, 0.5 is cyan, 0.6667 is             blue, 0.8333 is magenta, and 1 is red again!                          Saturation is the vibrancy of the color, where 0 is straight up a             shade of gray, and 1 is 'poke you in the eye colorful'.                          Value is the brightness of the color! 0 is always black.|
|float opacity|Also known as alpha! This is does not             affect the rgb components of the resulting color, it'll just get             slotted into the colors opacity value.|
|RETURNS: Color|A gamma space RGB color!|


## Color.LAB
```csharp
static Color LAB(float l, float a, float b, float opacity)
```
Creates a gamma space RGB color from a CIE-L*ab color
space. CIE-L*ab is a color space that models human perception,
and has significantly more accurate to perception lightness
values, so this is an excellent color space for color operations
that wish to preserve color brightness properly.

Traditionally, values are L [0,100], a,b [-200,+200] but here we
normalize them all to the 0-1 range. If you hate it, let me know
why!

|  |  |
|--|--|
|float l|Lightness of the color! Range is 0-1.|
|float a|'a' is from red to green. Range is 0-1.|
|float b|'b' is from blue to yellow. Range is 0-1.|
|float opacity|The opacity copied into the final color!|
|RETURNS: Color|A gamma space RGBA color constructed from the LAB values.|
```csharp
static Color LAB(Vec3 lab, float opacity)
```
Creates a gamma space RGB color from a CIE-L*ab color
space. CIE-L*ab is a color space that models human perception,
and has significantly more accurate to perception lightness
values, so this is an excellent color space for color operations
that wish to preserve color brightness properly.

Traditionally, values are L [0,100], a,b [-200,+200] but here we
normalize them all to the 0-1 range. If you hate it, let me know
why!

|  |  |
|--|--|
|Vec3 lab|For convenience, XYZ is LAB.             Lightness of the color! Range is 0-1.             'a' is from red to green. Range is 0-1.             'b' is from blue to yellow. Range is 0-1.|
|float opacity|The opacity copied into the final color!|
|RETURNS: Color|A gamma space RGBA color constructed from the LAB values.|


## Color.Hex
```csharp
static Color Hex(uint hexValue)
```
Create a color from an integer based hex value! This can
make it easier to copy over colors from the web. This isn't a
string function though, so you'll need to fill it out the whole
way. Ex: `Color.Hex(0x0000FFFF)` would be RGBA(0,0,1,1).

|  |  |
|--|--|
|uint hexValue|An integer representing RGBA hex values!             Like: `0x0000FFFF`.|
|RETURNS: Color|A 128 bit Color value.|


## Color.ToLinear
```csharp
Color ToLinear()
```
Converts this from a gamma space color, into a linear
space color! If this is not a gamma space color, this will just
make your color wacky!

|  |  |
|--|--|
|RETURNS: Color|A linear space color.|


## Color.ToGamma
```csharp
Color ToGamma()
```
Converts this from a linear space color, into a gamma
space color! If this is not a linear space color, this will just
make your color wacky!

|  |  |
|--|--|
|RETURNS: Color|A gamma space color.|


## Color.ToString
```csharp
string ToString()
```
Mostly for debug purposes, this is a decent way to log or
inspect the color in debug mode. Looks like "[r, g, b, a]"

|  |  |
|--|--|
|RETURNS: string|A string that looks like "[r, g, b, a]"|

# static class Default

This is a collection of StereoKit default assets that are
created or loaded by StereoKit during its initialization phase! Feel
free to use them or Copy them, but be wary about modifying them,
since it could affect many things throughout the system.

## Default.Material

```csharp
static Material Material{ get set }
```

The default material! This is used by many models and
meshes rendered from within StereoKit. Its shader is tuned for
high performance, and may change based on system performance
characteristics, so it can be great to copy this one when
creating your own materials! Or if you want to override
StereoKit's default material, here's where you do it!

## Default.MaterialPBR

```csharp
static Material MaterialPBR{ get set }
```

The default Physically Based Rendering material! This is
used by StereoKit anytime a mesh or model has metallic or
roughness properties, or needs to look more realistic. Its shader
may change based on system performance characteristics, so it can
be great to copy this one when creating your own materials! Or if
you want to override StereoKit's default PBR behavior, here's
where you do it! Note that the shader used by default here is
much more costly than Default.Material.

## Default.MaterialPBRClip

```csharp
static Material MaterialPBRClip{ get set }
```

Same as MaterialPBR, but it uses a discard clip for
transparency.

## Default.MaterialUnlit

```csharp
static Material MaterialUnlit{ get set }
```

The default unlit material! This is used by StereoKit
any time a mesh or model needs to be rendered with an unlit
surface. Its shader may change based on system performance
characteristics, so it can be great to copy this one when
creating your own materials! Or if you want to override
StereoKit's default unlit behavior, here's where you do it!

## Default.MaterialUnlitClip

```csharp
static Material MaterialUnlitClip{ get set }
```

The default unlit material with alpha clipping! This is
used by StereoKit for unlit content with transparency, where
completely transparent pixels are discarded. This means less
alpha blending, and fewer visible alpha blending issues! In
particular, this is how Sprites are drawn. Its shader may change
based on system performance characteristics, so it can be great
to copy this one when creating your own materials! Or if you want
to override StereoKit's default unlit clipped behavior, here's
where you do it!

## Default.MaterialEquirect

```csharp
static Material MaterialEquirect{ get set }
```

This material is used for projecting equirectangular
textures into cubemap faces. It's probably not a great idea to
change this one much!

## Default.MaterialFont

```csharp
static Material MaterialFont{ get set }
```

This material is used as the default for rendering text!
By default, it uses the 'default/shader_font' shader, which is a
two-sided alpha-clip shader. This also turns off backface culling.

## Default.MaterialHand

```csharp
static Material MaterialHand{ get set }
```

This is the default material for rendering the hand!
It's a copy of the default material, but set to transparent, and
using a generated texture.

## Default.MaterialUI

```csharp
static Material MaterialUI{ get set }
```

The material used by the UI! By default, it uses a shader
that creates a 'finger shadow' that shows how close the finger is
to the UI.

## Default.MaterialUIBox

```csharp
static Material MaterialUIBox{ get set }
```

A material for indicating interaction volumes! It
renders a border around the edges of the UV coordinates that will
'grow' on proximity to the user's finger. It will discard pixels
outside of that border, but will also show the finger shadow.
This is meant to be an opaque material, so it works well for
depth LSR.

This material works best on cube-like meshes where each face has
UV coordinates from 0-1.

Shader Parameters:
```color                - color
border_size          - meters
border_size_grow     - meters
border_affect_radius - meters```

## Default.MaterialUIQuadrant

```csharp
static Material MaterialUIQuadrant{ get set }
```

The material used by the UI for Quadrant Sized UI
elements. See UI.QuadrantSizeMesh for additional details. By
default, it uses a shader that creates a 'finger shadow' that shows
how close the finger is to the UI.

## Default.Tex

```csharp
static Tex Tex{ get set }
```

Default 2x2 white opaque texture, this is the texture
referred to as 'white' in the shader texture defaults.

## Default.TexBlack

```csharp
static Tex TexBlack{ get set }
```

Default 2x2 black opaque texture, this is the texture
referred to as 'black' in the shader texture defaults.

## Default.TexGray

```csharp
static Tex TexGray{ get set }
```

Default 2x2 middle gray (0.5,0.5,0.5) opaque texture,
this is the texture referred to as 'gray' in the shader texture
defaults.

## Default.TexFlat

```csharp
static Tex TexFlat{ get set }
```

Default 2x2 flat normal texture, this is a normal that
faces out from the, face, and has a color value of (0.5,0.5,1).
This is the texture referred to as 'flat' in the shader texture
defaults.

## Default.TexRough

```csharp
static Tex TexRough{ get set }
```

Default 2x2 roughness color (1,1,0,1) texture, this is the
texture referred to as 'rough' in the shader texture defaults.

## Default.TexDevTex

```csharp
static Tex TexDevTex{ get set }
```

This is a white checkered grid texture used to easily add
visual features to materials. By default, this is used for the
loading fallback texture for all Tex objects.

## Default.TexError

```csharp
static Tex TexError{ get set }
```

This is a red checkered grid texture used to indicate some
sort of error has occurred. By default, this is used for the error
fallback texture for all Tex objects.

## Default.Cubemap

```csharp
static Tex Cubemap{ get set }
```

The default cubemap that StereoKit generates, this is
the cubemap that's visible as the background and initial scene
lighting.

## Default.MeshQuad

```csharp
static Mesh MeshQuad{ get set }
```

A default quad mesh, 2 triangles, 4 verts, from
(-0.5,-0.5,0) to (0.5,0.5,0) and facing forward on the Z axis
(0,0,-1). White vertex colors, and UVs from (1,1) at vertex
(-0.5,-0.5,0) to (0,0) at vertex (0.5,0.5,0).

## Default.MeshScreenQuad

```csharp
static Mesh MeshScreenQuad{ get set }
```

A default quad mesh designed for full-screen rendering.
2 triangles, 4 verts, from (-1,-1,0) to (1,1,0) and facing
backwards on the Z axis (0,0,1). White vertex colors, and UVs
from (0,0) at vertex (-1,-1,0) to (1,1) at vertex (1,1,0).

## Default.MeshCube

```csharp
static Mesh MeshCube{ get set }
```

A cube with dimensions of (1,1,1), this is equivalent to
Mesh.GenerateCube(Vec3.One).

## Default.MeshSphere

```csharp
static Mesh MeshSphere{ get set }
```

A sphere mesh with a diameter of 1. This is equivalent
to Mesh.GenerateSphere(1,4).

## Default.Shader

```csharp
static Shader Shader{ get set }
```

This is a fast, general purpose shader. It uses a
texture for 'diffuse', a 'color' property for tinting the
material, and a 'tex_scale' for scaling the UV coordinates. For
lighting, it just uses a lookup from the current cubemap.

## Default.ShaderPbr

```csharp
static Shader ShaderPbr{ get set }
```

A physically based shader.

## Default.ShaderPbrClip

```csharp
static Shader ShaderPbrClip{ get set }
```

Same as ShaderPBR, but with a discard clip for
transparency.

## Default.ShaderUnlit

```csharp
static Shader ShaderUnlit{ get set }
```

Sometimes lighting just gets in the way! This is an
extremely simple and fast shader that uses a 'diffuse' texture
and a 'color' tint property to draw a model without any lighting
at all!

## Default.ShaderUnlitClip

```csharp
static Shader ShaderUnlitClip{ get set }
```

Sometimes lighting just gets in the way! This is an
extremely simple and fast shader that uses a 'diffuse' texture
and a 'color' tint property to draw a model without any lighting
at all! This shader will also discard pixels with an alpha of
zero.

## Default.ShaderFont

```csharp
static Shader ShaderFont{ get set }
```

A shader for text! Right now, this will render a font
atlas texture, and perform alpha-testing for transparency, and
super-sampling for better readability. It also flips normals of
the back-face of the surface, so  backfaces get lit properly when
backface culling is turned off, as it is by default for text.

## Default.ShaderEquirect

```csharp
static Shader ShaderEquirect{ get set }
```

A shader for projecting equirectangular textures onto
cube faces! This is for equirectangular texture loading.

## Default.ShaderUI

```csharp
static Shader ShaderUI{ get set }
```

A shader for UI or interactable elements, this'll be the
same as the Shader, but with an additional finger 'shadow' and
distance circle effect that helps indicate finger distance from
the surface of the object.

## Default.ShaderUIBox

```csharp
static Shader ShaderUIBox{ get set }
```

A shader for indicating interaction volumes! It renders
a border around the edges of the UV coordinates that will 'grow'
on proximity to the user's finger. It will discard pixels outside
of that border, but will also show the finger shadow. This is
meant to be an opaque shader, so it works well for depth LSR.

This shader works best on cube-like meshes where each face has
UV coordinates from 0-1.

Shader Parameters:
```color                - color
border_size          - meters
border_size_grow     - meters
border_affect_radius - meters```

## Default.SoundClick

```csharp
static Sound SoundClick{ get set }
```

A default click sound that lasts for 300ms. It's a
procedurally generated sound based on a mouse press, with extra
low frequencies in it.

## Default.SoundUnclick

```csharp
static Sound SoundUnclick{ get set }
```

A default click sound that lasts for 300ms. It's a
procedurally generated sound based on a mouse release, with extra
low frequencies in it.

## Default.Font

```csharp
static Font Font{ get set }
```

The default font used by StereoKit's text. This varies
from platform to platform, but is typically a sans-serif general
purpose font, such as Segoe UI.
# struct GradientKey

A color/position pair for Gradient values!

## GradientKey.color

```csharp
Color color
```

The color for this item, preferably in some form of
linear color space. Gamma corrected colors will definitely not
math correctly.

## GradientKey.position

```csharp
float position
```

Typically a value between 0-1! This is the position of
the color along the 'x-axis' of the gradient.

## GradientKey.GradientKey
```csharp
void GradientKey(Color colorLinear, float position)
```
A basic copy constructor for GradientKey.

|  |  |
|--|--|
|Color colorLinear|The color for this item, preferably in             some form of linear color space. Gamma corrected colors will             definitely not math correctly.|
|float position|Typically a value between 0-1! This is the             position of the color along the 'x-axis' of the gradient.|

# class Gradient

A Gradient is a sparse collection of color keys that are
used to represent a ramp of colors! This class is largely just
storing colors and allowing you to sample between them.

Since the Gradient is just interpolating values, you can use whatever
color space you want here, as long as it's linear and not gamma!
Gamma space RGB can't math properly at all. It can be RGB(linear),
HSV, LAB, just remember which one you have, and be sure to convert it
appropriately later. Data is stored as float colors, so this'll be a
high accuracy blend!

## Gradient.Count

```csharp
int Count{ get }
```

The number of color keys present in this gradient.

## Gradient.Gradient
```csharp
void Gradient()
```
Creates a new, completely empty gradient.
```csharp
void Gradient(GradientKey[] keys)
```
Creates a new gradient from the list of color keys!

|  |  |
|--|--|
|GradientKey[] keys|These can be in any order that you like, they'll             be sorted by their GradientKey.position value regardless!|


## Gradient.Add
```csharp
void Add(Color colorLinear, float position)
```
This adds a color key into the list. It'll get inserted
to the right slot based on its position.

|  |  |
|--|--|
|Color colorLinear|Any linear space color you like!|
|float position|Typically a value between 0-1! This is the              position of the color along the 'x-axis' of the gradient.|


## Gradient.Set
```csharp
void Set(int index, Color colorLinear, float position)
```
Updates the color key at the given index! This will NOT
re-order color keys if they are moved past another key's position,
which could lead to strange behavior.

|  |  |
|--|--|
|int index|Index of the color key to change.|
|Color colorLinear|Any linear space color you like!|
|float position|Typically a value between 0-1! This is the              position of the color along the 'x-axis' of the gradient.|


## Gradient.Remove
```csharp
void Remove(int index)
```
Removes the color key at the given index!

|  |  |
|--|--|
|int index|Index of the color key to remove.|


## Gradient.Get
```csharp
Color Get(float at)
```
Samples the gradient's color at the given position!

|  |  |
|--|--|
|float at|Typically a value between 0-1, but if you used             larger or smaller values for your color key's positions, it'll be             in that range!|
|RETURNS: Color|The interpolated color at the given position. If 'at' is smaller or larger than the gradient's position range, then the color will be clamped to the color at the beginning or end of the gradient!|


## Gradient.Get32
```csharp
Color32 Get32(float at)
```
Samples the gradient's color at the given position, and
converts it to a 32 bit color. If your RGBA color values are
outside of the 0-1 range, then you'll get some issues as they're
converted to 0-255 range bytes!

|  |  |
|--|--|
|float at|Typically a value between 0-1, but if you used             larger or smaller values for your color key's positions, it'll be             in that range!|
|RETURNS: Color32|The interpolated 32 bit color at the given position. If 'at' is smaller or larger than the gradient's position range, then the color will be clamped to the color at the beginning or end of the gradient!|

# static class Platform

This class provides some platform related code that runs
cross-platform. You might be able to do many of these things with C#,
but you might not be able to do them in as portable a manner as these
methods do!

## Platform.ForceFallbackKeyboard

```csharp
static bool ForceFallbackKeyboard{ get set }
```

Force the use of StereoKit's built-in fallback keyboard
instead of the system keyboard. This may be great for testing or
look and feel matching, but the system keyboard should generally be
preferred for accessibility reasons.

## Platform.KeyboardVisible

```csharp
static bool KeyboardVisible{ get }
```

Check if a soft keyboard is currently visible. This may be
an OS provided keyboard or StereoKit's fallback keyboard, but will
not indicate the presence of a physical keyboard.

## Platform.KeyboardShow
```csharp
static void KeyboardShow(bool show, TextContext inputType)
```
Request or hide a soft keyboard for the user to type on.
StereoKit will surface OS provided soft keyboards where available,
and use a fallback keyboard when not. On systems with physical
keyboards, soft keyboards generally will not be shown if the user
has interacted with their physical keyboard recently.

|  |  |
|--|--|
|bool show|Tells whether or not to open or close the soft             keyboard.|
|TextContext inputType|Soft keyboards can change layout to             optimize for the type of text that's required. StereoKit will             request the soft keyboard layout that most closely represents the             TextContext provided.|


## Platform.FilePicker
```csharp
static void FilePicker(PickerMode mode, Action`1 onSelectFile, Action onCancel, String[] filters)
```
Starts a file picker window! This will create a native
file picker window if one is available in the current setup, and
if it is not, it'll create a fallback filepicker build using
StereoKit's UI.

Flatscreen apps will show traditional file pickers, and UWP has
an OS provided file picker that works in MR. All others currently
use the fallback system.

A note for UWP apps, UWP generally does not have permission to
access random files, unless the user has chosen them with the
picker! This picker properly handles permissions for individual
files on UWP, but may have issues with files that reference other
files, such as .gltf files with external textures. See
Platform.WriteFile and Platform.ReadFile for manually reading and
writing files in a cross-platfom manner.

|  |  |
|--|--|
|PickerMode mode|Are we trying to Open a file, or Save a file?             This changes the appearance and behavior of the picker to support             the specified action.|
|Action`1 onSelectFile|This Action will be called with the             proper filename when the picker has successfully completed! On a             cancel or close event, this Action is not called.|
|Action onCancel|If the user cancels the file picker, or              the picker is closed via FilePickerClose, this Action is called.|
|String[] filters|A list of file extensions that the picker             should filter for. This is in the format of ".glb" and is case             insensitive.|
```csharp
static void FilePicker(PickerMode mode, Action`2 onComplete, String[] filters)
```
Starts a file picker window! This will create a native
file picker window if one is available in the current setup, and
if it is not, it'll create a fallback filepicker build using
StereoKit's UI.

Flatscreen apps will show traditional file pickers, and UWP has
an OS provided file picker that works in MR. All others currently
use the fallback system. Some pickers will block the system and
return right away, but others will stick around and let users
continue to interact with the app.

A note for UWP apps, UWP generally does not have permission to
access random files, unless the user has chosen them with the
picker! This picker properly handles permissions for individual
files on UWP, but may have issues with files that reference other
files, such as .gltf files with external textures. See
Platform.WriteFile and Platform.ReadFile for manually reading and
writing files in a cross-platfom manner.

|  |  |
|--|--|
|PickerMode mode|Are we trying to Open a file, or Save a file?             This changes the appearance and behavior of the picker to support             the specified action.|
|Action`2 onComplete|This action will be called when the file             picker has finished, either via a cancel event, or from a confirm             event. First parameter is a bool, where true indicates the              presence of a valid filename, and false indicates a failure or              cancel event.|
|String[] filters|A list of file extensions that the picker             should filter for. This is in the format of ".glb" and is case             insensitive.|


## Platform.FilePickerClose
```csharp
static void FilePickerClose()
```
If the picker is visible, this will close it and
immediately trigger a cancel event for the active picker.


## Platform.FilePickerVisible

```csharp
static bool FilePickerVisible{ get }
```

This will check if the file picker interface is
currently visible. Some pickers will never show this, as they
block the application until the picker has completed.

## Platform.WriteFile
```csharp
static bool WriteFile(string filename, string data)
```
Writes a UTF-8 text file to the filesystem, taking
advantage of any permissions that may have been granted by
Platform.FilePicker.

|  |  |
|--|--|
|string filename|Path to the new file. Not affected by             Assets folder path.|
|string data|A string to write to the file. This gets             converted to a UTF-8 encoding.|
|RETURNS: bool|True on success, False on failure.|
```csharp
static bool WriteFile(string filename, Byte[] data)
```
Writes an array of bytes to the filesystem, taking
advantage of any permissions that may have been granted by
Platform.FilePicker.

|  |  |
|--|--|
|string filename|Path to the new file. Not affected by             Assets folder path.|
|Byte[] data|An array of bytes to write to the file.|
|RETURNS: bool|True on success, False on failure.|


## Platform.ReadFile
```csharp
static bool ReadFile(string filename, String& data)
```
Reads the entire contents of the file as a UTF-8 string,
taking advantage of any permissions that may have been granted by
Platform.FilePicker.

|  |  |
|--|--|
|string filename|Path to the file. Not affected by Assets             folder path.|
|String& data|A UTF-8 decoded string representing the             contents of the file. Will be null on failure.|
|RETURNS: bool|True on success, False on failure.|
```csharp
static bool ReadFile(string filename, Byte[]& data)
```
Reads the entire contents of the file as a byte array,
taking advantage of any permissions that may have been granted by
Platform.FilePicker.

|  |  |
|--|--|
|string filename|Path to the file. Not affected by Assets             folder path.|
|Byte[]& data|A raw byte array representing the contents of             the file. Will be null on failure.|
|RETURNS: bool|True on success, False on failure.|


## Platform.ReadFileText
```csharp
static string ReadFileText(string filename)
```
Reads the entire contents of the file as a UTF-8 string,
taking advantage of any permissions that may have been granted by
Platform.FilePicker. Returns null on failure.

|  |  |
|--|--|
|string filename|Path to the file. Not affected by Assets             folder path.|
|RETURNS: string|A UTF-8 decoded string if successful, null if not.|


## Platform.ReadFileBytes
```csharp
static Byte[] ReadFileBytes(string filename)
```
Reads the entire contents of the file as a byte array,
taking advantage of any permissions that may have been granted by
Platform.FilePicker. Returns null on failure.

|  |  |
|--|--|
|string filename|Path to the file. Not affected by Assets             folder path.|
|RETURNS: Byte[]|A raw byte array if successful, null if not.|

# struct SHLight

A light source used for creating SphericalHarmonics data.

## SHLight.directionTo

```csharp
Vec3 directionTo
```

Direction to the light source.

## SHLight.color

```csharp
Color color
```

Color of the light in linear space! Values here can
exceed 1.
# struct SphericalHarmonics

Spherical Harmonics are kinda like Fourier, but on a sphere.
That doesn't mean terribly much to me, and could be wrong, but check
out [here](http://www.ppsloan.org/publications/StupidSH36.pdf) for
more details about how Spherical Harmonics work in this context!

However, the more prctical thing is, SH can be a function that
describes a value over the surface of a sphere! This is particularly
useful for lighting, since you can basically store the lighting
information for a space in this value! This is often used for
lightmap data, or a light probe grid, but StereoKit just uses a
single SH for the entire scene. It's a gross oversimplification, but
looks quite good, and is really fast! That's extremely great when
you're trying to hit 60fps, or even 144fps.

## SphericalHarmonics.coefficient1

```csharp
Vec3 coefficient1
```

A set of RGB coefficients

## SphericalHarmonics.coefficient2

```csharp
Vec3 coefficient2
```

A set of RGB coefficients

## SphericalHarmonics.coefficient3

```csharp
Vec3 coefficient3
```

A set of RGB coefficients

## SphericalHarmonics.coefficient4

```csharp
Vec3 coefficient4
```

A set of RGB coefficients

## SphericalHarmonics.coefficient5

```csharp
Vec3 coefficient5
```

A set of RGB coefficients

## SphericalHarmonics.coefficient6

```csharp
Vec3 coefficient6
```

A set of RGB coefficients

## SphericalHarmonics.coefficient7

```csharp
Vec3 coefficient7
```

A set of RGB coefficients

## SphericalHarmonics.coefficient8

```csharp
Vec3 coefficient8
```

A set of RGB coefficients

## SphericalHarmonics.coefficient9

```csharp
Vec3 coefficient9
```

A set of RGB coefficients

## SphericalHarmonics.DominantLightDirection

```csharp
Vec3 DominantLightDirection{ get }
```

Returns the dominant direction of the light represented
by this spherical harmonics data. The direction value is
normalized.

You can get the color of the light in this direction by using the
struct's Sample method:
`light.Sample(-light.DominantLightDirection)`.

## SphericalHarmonics.SphericalHarmonics
```csharp
void SphericalHarmonics(Vec3[] coefficients)
```
Creates a SphericalHarmonic from an array of
coefficients. Useful for loading stored data!

|  |  |
|--|--|
|Vec3[] coefficients|Must be an array with a length of 9!|


## SphericalHarmonics.ToArray
```csharp
Vec3[] ToArray()
```
Converts the SphericalHarmonic into an array of
coefficients 9 long. Useful for storing calculated data!

|  |  |
|--|--|
|RETURNS: Vec3[]|An array of coefficients 9 long.|


## SphericalHarmonics.Sample
```csharp
Color Sample(Vec3 normal)
```
Look up the color information in a particular direction!

|  |  |
|--|--|
|Vec3 normal|The direction to look in. Should be normalized.|
|RETURNS: Color|The Color represented by the SH in the given direction.|


## SphericalHarmonics.Add
```csharp
void Add(Vec3 lightDir, Color lightColor)
```
Adds a 'directional light' to the lighting approximation.
This can be used to bake a multiple light setup, or accumulate
light from a field of points.

|  |  |
|--|--|
|Vec3 lightDir|Direction to the light source.|
|Color lightColor|Color of the light, in linear color             space.|


## SphericalHarmonics.Brightness
```csharp
void Brightness(float scale)
```
Scales all the SphericalHarmonic's coefficients! This
behaves as if you're modifying the brightness of the lighting
this object represents.

|  |  |
|--|--|
|float scale|A multiplier for the coefficients! A value of             1 will leave everything the same, 0.5 will cut the brightness in             half, and a 2 will double the brightness.|


## SphericalHarmonics.FromLights
```csharp
static SphericalHarmonics FromLights(SHLight[] directional_lights)
```
Creates a SphericalHarmonics approximation of the
irradiance given from a set of directional lights!

|  |  |
|--|--|
|SHLight[] directional_lights|A list of directional lights!|
|RETURNS: SphericalHarmonics|A SphericalHarmonics approximation of the irradiance given from a set of directional lights!|

# static class Time

This class contains time information for the current session and frame!

## Time.Total

```csharp
static double Total{ get }
```

How much time has elapsed since StereoKit was initialized? 64 bit time precision.

## Time.Totalf

```csharp
static float Totalf{ get }
```

How much time has elapsed since StereoKit was initialized? 32 bit time precision.

## Time.TotalUnscaled

```csharp
static double TotalUnscaled{ get }
```

How much time has elapsed since StereoKit was initialized? 64 bit time precision.
This version is unaffected by the Time.Scale value!

## Time.TotalUnscaledf

```csharp
static float TotalUnscaledf{ get }
```

How much time has elapsed since StereoKit was initialized? 32 bit time precision.
This version is unaffected by the Time.Scale value!

## Time.Elapsed

```csharp
static double Elapsed{ get }
```

How much time elapsed during the last frame? 64 bit time precision.

## Time.Elapsedf

```csharp
static float Elapsedf{ get }
```

How much time elapsed during the last frame? 32 bit time precision.

## Time.ElapsedUnscaled

```csharp
static double ElapsedUnscaled{ get }
```

How much time elapsed during the last frame? 64 bit time precision.
This version is unaffected by the Time.Scale value!

## Time.ElapsedUnscaledf

```csharp
static float ElapsedUnscaledf{ get }
```

How much time elapsed during the last frame? 32 bit time precision.
This version is unaffected by the Time.Scale value!

## Time.Scale

```csharp
static double Scale{ set }
```

Time is scaled by this value! Want time to pass slower? Set it to 0.5! Faster? Try 2!

## Time.SetTime
```csharp
static void SetTime(double totalSeconds, double frameElapsedSeconds)
```
This allows you to override the application time! The application
will progress from this time using the current timescale.

|  |  |
|--|--|
|double totalSeconds|What time should it now be? The app will progress from this point in time.|
|double frameElapsedSeconds|How long was the previous frame? This is a number often used             in motion calculations. If left to zero, it'll use the previous frame's time, and if the previous             frame's time was also zero, it'll use 1/90.|
# Using Hands

StereoKit uses a hands first approach to user input! Even when hand-sensors
aren't available, hand data is simulated instead using existing devices!
For example, Windows Mixed Reality controllers will blend between pre-recorded
hand poses based on button presses, as will mice. This way, fully articulated
hand data is always present for you to work with!

## Accessing Joints

![Hand with joints]({{site.url}}/img/screenshots/HandAxes.jpg)

Since hands are so central to interaction, accessing hand information needs
to be really easy to get! So here's how you might find the fingertip of the right
hand! If you ignore IsTracked, this'll give you the last known position for that
finger joint.
```csharp
Hand hand = Input.Hand(Handed.Right);
if (hand.IsTracked)
{ 
	Vec3 fingertip = hand[FingerId.Index, JointId.Tip].position;
}
```
Pretty straightforward! And if you prefer calling a function instead of using the
[] operator, that's cool too! You can call `hand.Get(FingerId.Index, JointId.Tip)`
instead!

If that's too granular for you, there's easy ways to check for pinching and
gripping! Pinched will tell you if a pinch is currently happening, JustPinched
will tell you if it just started being pinched this frame, and JustUnpinched will
tell you if the pinch just stopped this frame!
```csharp
if (hand.IsPinched) { }
if (hand.IsJustPinched) { }
if (hand.IsJustUnpinched) { }

if (hand.IsGripped) { }
if (hand.IsJustGripped) { }
if (hand.IsJustUngripped) { }
```
These are all convenience functions wrapping the `hand.pinchState` bit-flag, so you
can also use that directly if you want to do some bit-flag wizardry!
## Hand Menu

Lets imagine you want to make a hand menu, you might need to know
if the user is looking at the palm of their hand! Here's a quick
example of using the palm's pose and the dot product to determine
this.
```csharp
static bool HandFacingHead(Handed handed)
{
	Hand hand = Input.Hand(handed);
	if (!hand.IsTracked)
		return false;

	Vec3 palmDirection   = (hand.palm.Forward).Normalized;
	Vec3 directionToHead = (Input.Head.position - hand.palm.position).Normalized;

	return Vec3.Dot(palmDirection, directionToHead) > 0.5f;
}
```
Once you have that information, it's simply a matter of placing a
window off to the side of the hand! The palm pose Right direction
points to different sides of each hand, so a different X offset
is required for each hand.
```csharp
public static void DrawHandMenu(Handed handed)
{
	if (!HandFacingHead(handed))
		return;

	// Decide the size and offset of the menu
	Vec2  size   = new Vec2(4, 16);
	float offset = handed == Handed.Left ? -2-size.x : 2+size.x;

	// Position the menu relative to the side of the hand
	Hand hand   = Input.Hand(handed);
	Vec3 at     = hand[FingerId.Little, JointId.KnuckleMajor].position;
	Vec3 down   = hand[FingerId.Little, JointId.Root        ].position;
	Vec3 across = hand[FingerId.Index,  JointId.KnuckleMajor].position;

	Pose menuPose = new Pose(
		at,
		Quat.LookAt(at, across, at-down) * Quat.FromAngles(0, handed == Handed.Left ? 90 : -90, 0));
	menuPose.position += menuPose.Right * offset * U.cm;
	menuPose.position += menuPose.Up * (size.y/2) * U.cm;

	// And make a menu!
	UI.WindowBegin("HandMenu", ref menuPose, size * U.cm, UIWin.Empty);
	UI.Button("Test");
	UI.Button("That");
	UI.Button("Hand");
	UI.WindowEnd();
}
```
## Pointers

And lastly, StereoKit also has a pointer system! This applies to
more than just hands. Head, mouse, and other devices will also
create pointers into the scene. You can filter pointers based on
source family and device capabilities, so this is a great way to
abstract a few more input sources nicely!
```csharp
public static void DrawPointers()
{
	int hands = Input.PointerCount(InputSource.Hand);
	for (int i = 0; i < hands; i++)
	{
		Pointer pointer = Input.Pointer(i, InputSource.Hand);
		Lines.Add    (pointer.ray, 0.5f, Color.White, Units.mm2m);
		Lines.AddAxis(pointer.Pose);
	}
}
```
The code in context for this document can be found [on Github here](https://github.com/StereoKit/StereoKit/blob/master/Examples/StereoKitTest/DemoHands.cs)!
# Using QR Codes

QR codes are a super fast and easy way to locate an object,
provide information from the environment, or `localize` two
devices to the same coordinate space! HoloLens 2 and WMR headsets
have a really convenient way to grab and use this data. They can use
the tracking cameras of the device, at the driver level to provide
QR codes from the environment, pretty much for free!

The only caveat is that tracking cameras are lower resolution, so
they need big QR codes, or to be very close to the codes. They also
only update around 2 times a second. But if that suits your needs?
Then you're in luck!

## Pre-Requisites

QR code support is not built directly in to StereoKit, but
it is quite trivial to implement! For this, we use the
[Microsoft MixedReality QR code library](https://docs.microsoft.com/en-us/windows/mixed-reality/qr-code-tracking)
through its NuGet package. This will require a UWP StereoKit
project, and the Webcam capability in the project's
.appxmanifest file.

So! That's the pre-reqs for this guide!

 - A StereoKit UWP project.
 - The [NuGet package](https://www.nuget.org/Packages/Microsoft.MixedReality.QR).
 - Enable the `Webcam` capability in the .appxmanifest file.

Then in your code, you'll be able to add this using
statement and get access to the `QRCodeWatcher`, the main
interface to the QR code functionality.
```csharp
using Microsoft.MixedReality.QR;
```
## Code

For code, we'll start with our own representation of
what a QR code means. Nothing fancy, we just want to
show the orientation and contents of each code! So, pose,
size, and data as text.

We'll also include a function to convert the WMR QR code into
our own. The only fancy stuff happening here is grabbing the
Pose! The `SpatialGraphNodeId` contains a pose, but it's in
UWPs coordinate space. `Pose.FromSpatialNode` is a bridge
function that will convert from UWP's coordinates into our own.
```csharp
struct QRData
{ 
	public Pose   pose;
	public float  size;
	public string text;
	public static QRData FromCode(QRCode qr)
	{
		QRData result = new QRData();
		// It's not unusual for this to fail to find a pose, especially on
		// the first frame it's been seen.
		World.FromSpatialNode(qr.SpatialGraphNodeId, out result.pose);
		result.size = qr.PhysicalSideLength;
		result.text = qr.Data == null ? "" : qr.Data;
		return result;
	}
}
```
Ok, cool! Now here's the data we'll be tracking for this demo,
the `QRCodeWatcher` is the object that'll provide us QR data,
`watcherStart` will let us filter out QR codes from other sesions,
and `poses` is our list of unique QR codes that we can iterate through
and draw.
```csharp
QRCodeWatcher watcher;
DateTime      watcherStart;
Dictionary<Guid, QRData> poses = new Dictionary<Guid, QRData>();
```
Initialization is just a matter of asking for permission, and then
hooking up to the `QRCodeWatcher`'s events. `QRCodeWatcher.RequestAccessAsync`
is an async call, so you could re-arrange this code to be non-blocking!

You'll also notice there's some code here for filtering out QR codes.
The default behavior for the QR code library is to provide all QR
codes that it knows about, and that includes ones that were found
before the session began. We don't need that, so we're ignoring those.
```csharp
public void Initialize()
{
	// Ask for permission to use the QR code tracking system
	var status = QRCodeWatcher.RequestAccessAsync().Result;
	if (status != QRCodeWatcherAccessStatus.Allowed)
		return;

	// Set up the watcher, and listen for QR code events.
	watcherStart = DateTime.Now;
	watcher      = new QRCodeWatcher();
	watcher.Added   += (o, qr) => {
		// QRCodeWatcher will provide QR codes from before session start,
		// so we often want to filter those out.
		if (qr.Code.LastDetectedTime > watcherStart) 
			poses.Add(qr.Code.Id, QRData.FromCode(qr.Code)); 
	};
	watcher.Updated += (o, qr) => poses[qr.Code.Id] = QRData.FromCode(qr.Code);
	watcher.Removed += (o, qr) => poses.Remove(qr.Code.Id);
	watcher.Start();
}

// For shutdown, we just need to stop the watcher
public void Shutdown() => watcher?.Stop();

```
Now all we need to do is show the QR codes! In this case,
we're just displaying an axis widget, and the contents of
the QR code as text.

With the text, all we're doing is squeezing the text into
the bounds of the QR code, and shifting it to be a little
forward, in front of the code!
```csharp
public void Update()
{
	foreach(QRData d in poses.Values)
	{ 
		Lines.AddAxis(d.pose, d.size);
		Text .Add(
			d.text, 
			d.pose.ToMatrix(),
			Vec2.One * d.size,
			TextFit.Squeeze,
			TextAlign.XLeft | TextAlign.YTop,
			TextAlign.Center,
			d.size, d.size);
	}
}
```
And that's all there is to it! You can find all this code
in context [here on Github](https://github.com/StereoKit/StereoKit/blob/master/Examples/StereoKitTest/Demos/DemoQRCode.cs).
# Building UI in StereoKit

StereoKit uses an immediate mode UI system. Basically, you define the UI
every single frame you want to see it! Sounds a little odd at first, but
it does have some pretty tremendous advantages. Since very little state
is actually stored, you can add, remove, and change your UI elements with
trivial and standard code structures! You'll find that you often have
much less UI code, with far fewer places for things to go wrong.

The goal for this UI API is to get you up and running as fast as possible
with a working UI! This does mean trading some design flexibility for API
simplicity, but we also strive to retain configurability for those that
need a little extra.

## Making a Window

![Simple UI]({{site.url}}/img/screenshots/GuideUserInterface.jpg)

Since StereoKit doesn't store state, you'll have to keep track of
your data yourself! But that's actually a pretty good thing, since
you'll probably do that one way or another anyhow. Here we've got a
Pose for the window, off to the left and facing to the right, as well
as a boolean for a toggle, and a float that we'll use as a slider!
We'll add this code to our initialization section.
```csharp
Pose  windowPose = new Pose(-.4f, 0, 0, Quat.LookDir(1,0,1));

bool  showHeader = true;
float slider     = 0.5f;

Sprite powerSprite = Sprite.FromFile("power.png", SpriteType.Single);
```
Then we'll move over to the application step where we'll do the
rest of the UI code!

We'll start with a window titled "Window" that's 20cm wide, and
auto-resizes on the y-axis. The U class is pretty helpful here,
as it allows us to reason more visually about the units we're
using! StereoKit uses meters as its base unit, which look a
little awkward as raw floats, especially in the millimeter range.

We'll also use a toggle to turn the window's header on and off!
The value from that toggle is passed in here via the showHeader
field.

```csharp
UI.WindowBegin("Window", ref windowPose, new Vec2(20, 0) * U.cm, showHeader?UIWin.Normal:UIWin.Body);
```

When you begin a window, all visual elements are now relative to
that window! UI takes advantage of the Hierarchy class and pushes
the window's pose onto the Hierarchy stack. Ending the window
will pop the pose off the hierarchy stack, and return things to
normal!

Here's that toggle button! You'll also notice our use of 'ref'
values in a lot of the UI code. UI functions typically follow the
pattern of returning true/false to indicate they've been
interacted with during the frame, so you can nicely wrap them in
'if' statements to react to change!

Then with the 'ref' parameter, we let you pass in the current
state of the UI element. The UI element will update that value
for you based on user interaction, but you can also change it
yourself whenever you want to!

```csharp
UI.Toggle("Show Header", ref showHeader);
```

Here's an example slider! We start off with a label element, and
tell the UI to keep the next item on the same line. The slider
clamps to the range [0,1], and will step at intervals of 0.2. If
you want it to slide continuously, you can just set the `step`
value to 0!

```csharp
UI.Label("Slide");
UI.SameLine();
UI.HSlider("slider", ref slider, 0, 1, 0.2f, 72 * U.mm);
```

Here's how you use a simple button! Just check it with an 'if'.
Any UI method will return true on the frame when their value or
state has changed.

```csharp
if (UI.ButtonImg("Exit", powerSprite))
	SK.Quit();
```

And for every begin, there must also be an end! StereoKit will
log errors when this occurs, so keep your eyes peeled for that!

```csharp
UI.WindowEnd();
```

## Custom Windows

![Simple UI]({{site.url}}/img/screenshots/GuideUserInterfaceCustom.jpg)

Mixed Reality also provides us with the opportunity to turn
objects into interfaces! Instead of using the old 'window'
paradigm, we can create 3D models and apply UI elements to their
surface! StereoKit uses 'handles' to accomplish this, a grabbable
area that behaves much like a window, but with a few more options
for customizing layout and size.

We'll load up a clipboard, so we can attach an interface to that!

```csharp
Model clipboard = Model.FromFile("Clipboard.glb");
```

And, similar to the window previously, here's how you would turn
it into a grabbable interface! This behaves the same, except
we're defining where the grabbable region is specifically, and
then drawing our own model instead of a plain bar. You'll also
notice we're drawing using an identity matrix. This takes
advantage of how HandleBegin pushes the handle's pose onto the
Hierarchy transform stack!

```csharp
UI.HandleBegin("Clip", ref clipboardPose, clipboard.Bounds);
clipboard.Draw(Matrix.Identity);
```

Once we've done that, we also need to define the layout area of
the model, where UI elements will go. This is different for each
model, so you'll need to plan this around the size of your
object!

```csharp
UI.LayoutArea(new Vec3(12, 15, 0) * U.cm, new Vec2(24, 30) * U.cm);
```

Then after that? We can just add UI elements like normal!

```csharp
UI.Image(logoSprite, new Vec2(22,0) * U.cm);

UI.Toggle("Toggle", ref clipToggle);
UI.HSlider("Slide", ref clipSlider, 0, 1, 0, 22 * U.cm);
```

And while we're at it, here's a quick example of doing a radio
button group! Not much 'radio' actually happening, but it's still
pretty simple. Pair it with an enum, or an integer, and have fun!

```csharp
if (UI.Radio("Radio1", clipOption == 1)) clipOption = 1;
UI.SameLine();
if (UI.Radio("Radio2", clipOption == 2)) clipOption = 2;
UI.SameLine();
if (UI.Radio("Radio3", clipOption == 3)) clipOption = 3;
```

As with windows, Handles need an End call.

```csharp
UI.HandleEnd();
```

## An Important Note About IDs

StereoKit does store a small amount of information about the UI's
state behind the scenes, like which elements are active and for
how long. This internal data is attached to the UI elements via
a combination of their own ids, and the parent Window/Handle's
id!

This means you should be careful to NOT re-use ids within a
Window/Handle, otherwise you may find ghost interactions with
elements that share the same ids. If you need to have elements
with the same id, or if perhaps you don't know in advance that
all your elements will certainly be unique, UI.PushId and
UI.PopId can be used to mitigate the issue by using the same
hierarchy id mixing that the Windows use to prevent collisions
with the same ids in other Windows/Handles.

Here's the same set of radio options, but all of them have the
same name/id!

```csharp
UI.PushId(1);
if (UI.Radio("Radio", clipOption == 1)) clipOption = 1;
UI.PopId();

UI.SameLine();
UI.PushId(2);
if (UI.Radio("Radio", clipOption == 2)) clipOption = 2;
UI.PopId();

UI.SameLine();
UI.PushId(3);
if (UI.Radio("Radio", clipOption == 3)) clipOption = 3;
UI.PopId();
```
## What's Next?

And there you go! That's how UI works in StereoKit, pretty
simple, huh? For further reference, and more UI methods, check
out the [UI class documentation]({{site.url}}/Pages/Reference/UI.html).

If you'd like to see the complete code for this sample,
[check it out on Github](https://github.com/StereoKit/StereoKit/blob/master/Examples/StereoKitTest/Demos/DemoUI.cs)!
# Debugging your App
### Set up for debugging
Since StereoKit's core is composed of native code, there are a few extra steps you can take to get better stack traces and debug information! The first is to make sure Visual Studio is set up to debug with native code. This varies across .Net versions, but generally the option can be found at Project->Properties->Debug->(Native code debugging).

You may also wish to disable "Just My Code" if you're trying to actually inspect how StereoKit's code is behaving. This can be found under Tools->Options->Debugging->General->Enable Just My Code, and uncheck it to make sure it's disabled.

StereoKit is set up with Source Link as of v0.3.5, which allows you to inspect StereoKit's code directly from the relevant commit of the main repository on GitHub. Note that distributed binaries are in release format, and may not 'step through' as nicely as a normal debug binary would.

### Check the Logs!
StereoKit outputs a lot of useful information in the logs, and there's a chance your issue may be logged there! When submitting an issue on the GitHub repository, including a copy of your logs can really help maintainers to understand what is or isn't happening.

All platforms will output the log through the standard debug output window, but you can also tap into the debug logs via [`Log.Subscribe`]({{site.url}}/Pages/Reference/Log/Subscribe.html). Check the docs there for an easy Mixed Reality log window you can add to your project.

### Ask for Help
We love to hear what problems you're running into! StereoKit is completely open source and has no analytics or surveillance tools embedded in it at all. If you have an issue, we won't know about it unless _you_ tell us, or we spot it ourselves!

The best place to ask for help will always be the [GitHub Issues](https://github.com/StereoKit/StereoKit/issues), or [GitHub Discussions](https://github.com/StereoKit/StereoKit/discussions) pages. Be sure to provide logs, platform information, and as many other details as may be relevant!

## Common Issues
Here's a short list of some common issues we've seen people ask about!

### XR_ERROR_FORM_FACTOR_UNAVAILABLE in the logs
This is a common and expected message that basically means that OpenXR can't find any headset attached to the system. Your headset is most likely unplugged, but may also indicate some other issue with your OpenXR runtime setup.

By default, StereoKit will fall back to the flatscreen simulator when this error message is encountered. This behavior can be configured in your `SKSettings`.

### StereoKit isn't loading my asset!
This may manifest as Null Reference Exceptions surrounding your Model/Tex/asset. The first thing to do is check the StereoKit logs, and look for messages with your asset's filename. There will likely be some message with a decent hint available.

If StereoKit cannot find your file, make sure the path is correct, and verify your asset is correctly being copied into Visual Studio's output folder. The default templates will automatically copy content in the project's Assets folder into the final output folder. If your asset is not in the Assets folder, or if you have assembled your own project without using the templates, then you may need to do additional work to ensure the copy happens.

### System.DLLNotFoundException for StereoKitC
A StereoKit function has been called before the native StereoKit DLL was loaded. Make sure your code is happening _after_ your call to `SK.Initialize`! Watch out for code being called from implied constructors, especially on static classes.

For some rare cases where you need access to a StereoKit function before initialization, you may be able to call `SK.PreLoadLibrary`. This only works for functions that interact with code that does not require initialization, like math. It may also disguise code that's incorrectly being called before SK.Initialize.
# Drawing content with StereoKit

Generally, the first thing you want to do is get content on-screen! Or
in-visor? However it's said, in this guide we're going to explore the
various ways to display some holograms!

At its core, drawing things in 3D is done through a combination of
[`Mesh`]({{site.url}}/Pages/Reference/Mesh.html)es and
[`Material`]({{site.url}}/Pages/Reference/Material.html)s. A Mesh
is a collection of triangles in 3D space that describe where the
surface of that 3D object is. And a Material is then a collection
of parameters, [`Tex`]({{site.url}}/Pages/Reference/Tex.html)tures
(2d images), and Shader code that are combined to describe the
visual properties of the Mesh's surface!

![Meshes are made from triangles!]({{site.screen_url}}/Drawing_MeshLooksLike.jpg)
_Meshes are made from triangles!_

And in addition to that, you'll need to know a little bit about
matrices, which are a math construct used to describe the location,
orientation and scale of geometry within the 3D space! A [`Matrix`]({{site.url}}/Pages/Reference/Matrix.html)
isn't difficult the way we're using it, so don't worry if math
isn't your thing.

And then StereoKit also has a [`Model`]({{site.url}}/Pages/Reference/Model.html),
which is a high level combination of Meshes, Material, Matrices,
and a few more things! Most of the time, you'll probably be drawing
Models loaded from file, but it's important to have options.

Then lastly, StereoKit has easy systems for drawing [`Line`]({{site.url}}/Pages/Reference/Lines.html)s,
[`Text`]({{site.url}}/Pages/Reference/Text.html), [`Sprite`]({{site.url}}/Pages/Reference/Sprite.html)s
and various other things! These are still based on Meshes and
Materials under the hood, but have some complex features that can
make them difficult to build from scratch.

## Meshes and Materials

To simplify things here, we're going to use the built-in assets,
[`Mesh.Sphere`]({{site.url}}/Pages/Reference/Mesh/Sphere.html)
and [`Material.Default`]({{site.url}}/Pages/Reference/Material/Default.html).
Mesh.Sphere is a built-in mesh generated using math when StereoKit
starts up, and Material.Default is a high performance simple
Material that serves as StereoKit's default Material. (For more
built-in assets, see the [`Default`]({{site.url}}/Pages/Reference/Default.html)s)

```csharp
Mesh.Sphere.Draw(Material.Default, Matrix.Identity);
```

![Default sphere and material]({{site.screen_url}}/Drawing_Defaults.jpg)
_Drawing the default sphere Mesh with the default Material._

[`Matrix.Identity`]({{site.url}}/Pages/Reference/Matrix/Identity.html)
can be though of as a 'No transform' Matrix, so this is drawing the
sphere at the origin of the 3D space.

That's pretty straightforward! StereoKit will get this Mesh/Material
pair onto the screen this frame. Remember that StereoKit is
generally an immediate mode API, so this won't show up for more
than the current frame. If you want it to draw every frame, you'll
have to call Draw every frame!

So how do you get a Mesh to begin with? In most cases you'll just
be working with Models, but you can get a Mesh directly from a few
places:
 - [`Mesh.Sphere`]({{site.url}}/Pages/Reference/Mesh/Sphere.html), [`Mesh.Cube`]({{site.url}}/Pages/Reference/Mesh/Cube.html), and [`Mesh.Quad`]({{site.url}}/Pages/Reference/Mesh/Quad.html) are built-in mesh assets that are handy to have around.
 - [`Mesh`]({{site.url}}/Pages/Reference/Mesh.html) has a number of static methods for generating procedural shapes, such as [`Mesh.GenerateRoundedCube`]({{site.url}}/Pages/Reference/Mesh/GenerateRoundedCube.html) or [`Mesh.GeneratePlane`]({{site.url}}/Pages/Reference/Mesh/GeneratePlane.html).
 - A Mesh can be extracted from one of a [Model's nodes]({{site.url}}/Pages/Reference/ModelNode/Mesh.html).
 - You can create a Mesh from a list of vertices and indices. This is more advanced, but [check the sample here]({{site.url}}/Pages/Reference/Mesh/SetVerts.html).

And where do you get a Material? Well,
 - See built-in Materials like [`Material.PBR`]({{site.url}}/Pages/Reference/Default/MaterialPBR.html) for high-quality surface or [`Material.Unlit`]({{site.url}}/Pages/Reference/Default/MaterialUnlit.html) for fast/stylistic surfaces.
 - A Material [constructor]({{site.url}}/Pages/Reference/Material/Material.html) can be called with a Shader. Check out [the Material guide]({{site.url}}/Pages/Guides/Working-with-Materials.html) for in-depth usage (Materials and Shaders are a lot of fun!).
 - You can call [`Material.Copy`]({{site.url}}/Pages/Reference/Material/Copy.html) to create a duplicate of an existing Material.

## Matrix basics

If you like math, this explanation is not really for you! But if
you like results, this will get you going where you need to go. The
important thing to know about a [`Matrix`]({{site.url}}/Pages/Reference/Matrix.html),
is that it's a good way to represent an object's transform (Translation,
Rotation, and Scale).

StereoKit provides a number of Matrix creation methods that allow
you to easily create Translation/Rotation/Scale matrices.
```csharp
// The identity matrix is the matrix equivalent of '1'. You can also
// think of it as a 'no-transform' matrix.
Matrix transform = Matrix.Identity;

// Translates points 1 meter up the Y axis
transform = Matrix.T(0, 1, 0);

// Scales a point by (2,2,2), rotates it 180 on the X axis, and
// then translates it up 1 meter up the Y axis.
transform = Matrix.TRS(
	new Vec3(0,1,0),
	Quat.FromAngles(180, 0, 0),
	new Vec3(2,2,2));

// To draw a cube at (0,-10,0) that's rotated 45 degrees around its Y
// axis:
Mesh.Cube.Draw(Material.Default, Matrix.TR(0,-10,0, Quat.FromAngles(0,45,0)));
```

The TRS methods have a lot of permutations that can help make your
matrix creation code a bit shorter. Like, if you don't need to add
scale to your TRS matrix, there's the TR variant! No rotation? Try
TS! Etc. etc.

Now. Even _more_ interesting, is that many Matrices can be combined
into a single Matrix representing multiple transforms! This is done
via multiplication, and an important note here is that matrix
multiplication is not commutative, that is: `A*B != B*A`, so the
order in which you combine your matrices is important.

This can let you do things like, rotate around a pivot point, or
build a hierarchy of transforms! A parent/child position hierarchy
can be described pretty easily this way:
```csharp
Matrix parentTransform = Matrix.TR(10, 0, 0, Quat.FromAngles(0, 45, 0));
Matrix childTransform  = Matrix.TS( 1, 1, 0, 0.2f);

Mesh.Cube.Draw(Material.Default, parentTransform);
Mesh.Cube.Draw(Material.Default, childTransform * parentTransform);
```

![Combining matrices]({{site.screen_url}}/Drawing_MatrixCombine.jpg)
_The smaller `childTransform` is relative to `parentTransform` via multiplication._

## Models

The easiest way to draw complex content is through a Model! A Model
is basically a small scene of Mesh/Material pairs at positions with
hierarchical relationships to each other. If you're creating art in
a 3D modeling tool such as Blender, then this is basically a full
representation of the scene you've created there.

Since a model already has all its information within it, all you
need to do is provide it with a location!
```csharp
model.Draw(Matrix.T(10, 10, 0));
```
![Drawing a model]({{site.screen_url}}/Drawing_Model.jpg)
_StereoKit's main format is the .gltf file._

So... that was also pretty simple! The only real trick with Models
is getting one in the first place, but even that's not too hard.
There's a lot you can do with a Model beyond just drawing it, so
for more details on that, check out [the Model guide](https://github.com/StereoKit/StereoKit/blob/master/Examples/StereoKitTest/Demos/DemoNodes.cs) (coming soon)!

But here's the quick list of where you can get a Model to begin
with:
 - [`Model.FromFile`]({{site.url}}/Pages/Reference/Model/FromFile.html) is the easiest, most common way to get a Model!
 - [`Model.FromMesh`]({{site.url}}/Pages/Reference/Model/FromMesh.html) will let you create a very simple Model with a single function call.
 - The [Model constructor]({{site.url}}/Pages/Reference/Model/Model.html) lets you create an empty Model, which you can then fill with ModelNodes via [`Model.AddNode`]({{site.url}}/Pages/Reference/Model/AddNode.html)
 - You can call [`Model.Copy`]({{site.url}}/Pages/Reference/Model/Copy.html) to create a duplicate of an existing Model.

## Lines

Being able to easily draw a line is incredibly useful for
debugging, and generally quite practical for many other purposes as
well! StereoKit has the [`Lines`]({{site.url}}/Pages/Reference/Lines.html)
class to assist with this, and is pretty straightforward to use.
There's a few variations, but at it's simplest, it's a few points,
a color, and a thickness.
```csharp
Lines.Add(
	new Vec3(2, 2, 0),
	new Vec3(3, 2.5f, 0),
	Color.Black, 1*U.cm);
```
![Drawing a line]({{site.screen_url}}/Drawing_Lines.jpg)
_You can also draw Rays, Poses, and multicolored lists of lines!_

## Text

Text is drawn with a collection of rectangular quads, each mapped
to a character glyph on a texture. StereoKit supports rendering any
Unicode glyphs you throw at it, as long as the active Font has
that glyph defined in it! This means you can work with all sorts of
different languages right away, without any baking or preparation.

To draw text with the default Font, you can do this!
```csharp
Text.Add("こんにちは", Matrix.T(-10, 10,0));
```

![Drawing text]({{site.screen_url}}/Drawing_Text.jpg)
_'Hello' in Japanese, I'm pretty sure._

You can create additional font styles and fonts to use with text
drawing, and there are a number of overloads for [`Text.Add`]({{site.url}}/Pages/Reference/Text/Add.html)
that allow you to change the layout or constrain to a particular
area. Check the docs for the method for more information about that!

## Cool!

So that's the highlights! There's plenty more to draw and more
tricks to be learned, but this is a great start! There's treasures
in the documentation, so hunt around in there for more samples. You
may also be interested in the [Materials guide]({{site.url}}/Pages/Guides/Working-with-Materials.html)
for advanced rendering code, or the [Model guide](https://github.com/StereoKit/StereoKit/blob/master/Examples/StereoKitTest/Demos/DemoNodes.cs)
(coming soon), for managing your visible content!

If you'd like to see all the code for this document,
[check it out here!](https://github.com/StereoKit/StereoKit/blob/master/Examples/StereoKitTest/Guides/GuideDrawing.cs)
# Getting Started with StereoKit

Here's a quick list of what you'll need to start developing with StereoKit:

- **[Visual Studio 2019 or 2022](https://visualstudio.microsoft.com/vs/) - Use these workloads:**
  - .NET Desktop development
  - Universal Windows Platform development (for HoloLens)
  - Mobile development with .Net (for Quest)
- **[StereoKit's Visual Studio Template](https://marketplace.visualstudio.com/items?itemName=NickKlingensmith.StereoKitTemplates)**
  - Experienced users might directly use the [NuGet package](https://www.nuget.org/packages/StereoKit).
- **Any OpenXR runtime**
  - A flatscreen fallback is available for development.
- **Enable Developer Mode (for UWP/HoloLens)**
  - Windows Settings->Update and Security->For Developers->Developer Mode

This short video goes through the pre-requisites for building StereoKit's
hello world! You can find a [UWP/HoloLens specific version here](https://www.youtube.com/watch?v=U_7VNIcPQaM)
as well.
<iframe width="560" height="315" src="https://www.youtube-nocookie.com/embed/lOYs8seoRpc" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>

For Mac developers: while StereoKit's _simulator_ does not run on Mac OS,
you can still deploy to standalone Android headsets such as Quest!
[See here](https://www.youtube.com/watch?v=UMwTLecVATU) for a quick video
by community member Raphael about how to do this with the experimental
cross platform template.

## The Templates

![Create New Project]({{site.url}}/img/screenshots/VSNewProject.png)

- **StereoKit .Net Core**
  - .Net Core is for desktop XR on Windows and Linux. It is simple, compiles quickly, and is the best option for most developers.
- **StereoKit UWP**
  - UWP is for HoloLens 2, and can run on Windows desktop. UWP can be slower to compile, and is no longer receiving updates from the .Net team.
- _[Cross Platform/Universal Template (in development)](https://github.com/StereoKit/SKTemplate-Universal)_
  - This is an early version still in project format. It works with .Net Core, UWP, and Xamarin(Android/Quest) all at once via a DLL shared between multiple platform specific projects.
- **[Native C/C++ Template](https://github.com/StereoKit/SKTemplate-CMake)**
  - StereoKit does provide a C API, but experienced developers should only choose this if the benefits outweigh the lack of C API documentation.

For an overview of the initial code in the .Net Core and UWP templates,
check out this video!
<iframe width="560" height="315" src="https://www.youtube-nocookie.com/embed/apcWlHNJ5kM" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>

## Minimum "Hello Cube" Application

The template does provide some code to help provide new developers a base
to work from, but what parts of the code are really necessary? We can boil
"Hello Cube" down to something far simpler if we want to! This is the
simplest possible StereoKit application:

```csharp
using StereoKit;

class Program
{
	static void Main(string[] args)
	{
		SK.Initialize(new SKSettings{ appName = "Project" });

		SK.Run(() =>
		{
			Mesh.Cube.Draw(Material.Default, Matrix.S(0.1f));
		});
	}
}
```

## Next Steps

Awesome! That's pretty great, but what next? [Why don't we build some UI]({{site.url}}/Pages/Guides/User-Interface.html)?
Alternatively, you can check out the [StereoKit Ink](https://github.com/StereoKit/StereoKit-PaintTutorial)
repository, which contains an XR ink-painting application written in about
220 lines of code! It's well commented, and is a good example to pick
through.

For additional learning resources, you can check out the [Learning Resources]({{site.url}}/Pages/Guides/Learning-Resources.html)
page for a couple of repositories and links that may help you out. In
particular, the GitHub repository does contain a [number of small demo scenes](https://github.com/StereoKit/StereoKit/tree/master/Examples/StereoKitTest/Demos)
that are excellent reference for a number of different StereoKit features!

And don't forget to peek in the docs here! Most pages contain sample code
that illustrates how a particular function or property is used
in-context. The ultimate goal is to have a sample for 100% of the docs,
so if you're looking for one and it isn't there, use the 'Create an Issue'
link at the bottom of the web page to get it prioritized!
# Learning Resources

Outside of the resources here on the StereoKit site, there's a number of
other places you can go for learning information! Here's a collection of
external learning and sample resources to get you off the ground a little
faster! If you have your own resources that you'd like to see linked
here, just let us know!

## Sample Projects

### [StereoKit Ink](https://github.com/StereoKit/StereoKit-PaintTutorial)

![StereoKit Ink]({{site.screen_url}}/StereoKitInk.jpg)

A well documented repository that illustrates creating a complete but
simplified inking application. It includes functionality like custom and
standard UI, line rendering, file save/load, and hand menus.

### [Bing Maps API and Terrain Sample](https://github.com/StereoKit/StereoKit-BingMaps)

![Bing Maps API and Terrain Sample]({{site.screen_url}}/SKMapsTutorial.jpg)

A well documented repository showing how to load and display satellite
imagery and elevation information from the Bing Maps API. It includes
creating a terrain system using StereoKit's shader API, loading color and
height data from an external API, and building a pedestal interface to
interact with the content.

### [Release Notes Demo for v0.3.1](https://github.com/StereoKit/StereoKitReleaseNotes/tree/main/v0.3.1)

This is an interactive release notes demo project that showcases the
features released in StereoKit v0.3.1! Not every release has a demo like
this, but it can be pretty enlightening to browse through a code-base
such as this one for reference.

### [GitHub Demos folder](https://github.com/StereoKit/StereoKit/tree/master/Examples/StereoKitTest/Demos)

These are the demos I build to test StereoKit features and APIs! They
are occasionally documented, but frequently short and concise. They
can be great to check out for a focused example of certain parts of
the API!

### [GitHub Discussions/Issues](https://github.com/StereoKit/StereoKit/discussions)

The best place to ask a question! It's asynchronous, and a great place
for long-form answers that can also benefit others. The Discussions tab
is best for questions, feedback, and more nebulous stuff, and the Issues
tab is best if you think something might be misbehaving or missing!

### [The StereoKit Discord Channel](https://discord.gg/jtZpfS7nyK)

In a rush with a question, got a project to share, or just want to hang
out and chat? Or maybe you're looking for some feedback on a potential
contribution? Whatever the case, come and say hi on the Discord! This is
the core hang-out spot for the team and community :)

# Using the Simulator

As a developer, you can't realistically spend all of your development in
a headset just yet. So, a decent grasp over StereoKit's fallback
flatscreen MR simulator is particularly helpful! This is basically a 2D
window that allows you to move around and interact, without requiring an
OpenXR runtime or headset.

## Simulator Controls

When you start the simulator, you'll find that your mouse controls the
right hand by default. This is a complete simulation of an articulated
hand, so you'll have access to all the joints the same way you would a
real tracked hand. The hand becomes tracked when the mouse enters the
window, and untracked when leaving the window. The pointer ray, which is
normally a shoulder->hand ray, will be along the mouse ray instead.

### Mouse Controls:
- Left Mouse - Hand animates to a Pinch gesture.
- Right Mouse - Hand animates to a Grip gesture.
- Left + Right - Hand animates to a closed fist.
- Scroll Wheel - Moves the hand toward or away from the user.
- Shift + Right - Mouse-look / rotate the head.
- Left Alt - [Eye tracking](({{site.url}}/Pages/Reference/Input/Eyes.html) will point along the ray indicated by the mouse.

To move around in space, you'll find controls that should be familiar to
those that play first-person games! Hold Left Shift to enable this.

### Keyboard Controls:
- Shift+W - Move forward.
- Shift+A - Move left.
- Shift+S - Move backwards.
- Shift+D - Move right.
- Shift+Q - Move down.
- Shift+E - Move up.

## Simulator API

There's a few bits of functionality that let you set up the simulator, or
some features that may assist you in debugging or testing! Here's a
couple you may want to know about:

### Simulator Enable/Disable

By default, StereoKit will fall back to the flatscreen simulator if
OpenXR fails to initialize for any reason (like, headset not plugged in,
or OpenXR not present). You can modify this behavior at initialization
time when defining your SKSettings for SK.Init.
```csharp
SKSettings settings = new SKSettings {
	appName                = "Flatscreen Simulator",
	assetsFolder           = "Assets",
	// This tells StereoKit to always start in a 2D flatscreen
	// window, instead of an immersive MR environment.
	displayPreference      = DisplayMode.Flatscreen,
	// Setting this to true will disable all built-in MR simulator
	// controls.
	disableFlatscreenMRSim = false,
	// Setting this to true will prevent StereoKit from creating the
	// fallback simulator when OpenXR fails to initialize. This is
	// important when shipping a final application to users.
	noFlatscreenFallback   = true,
};
```

### Overriding Hands

A number of functions are present that can make unit test and
complex input simulation possible. For a full example of this,
the [DebugToolWindow](https://github.com/StereoKit/StereoKit/blob/master/Examples/StereoKitTest/DebugToolWindow.cs)
in the Test project has a number of sample utilities for
recording and playing back input.

Overriding the hands is one important element that you may want
to do! [`Input.HandOverride`]({{site.url}}/Pages/Reference/Input/HandOverride.html)
will set the hand input to a very specific pose, and hold that
pose until you call `Input.HandOverride` again with a new pose,
or call [`Input.HandClearOverride`]({{site.url}}/Pages/Reference/Input/HandClearOverride.html)
to restore control back to the user.

![An overridden hand]({{site.screen_url}}/HandOverride.jpg)
_This screenshot is generated fresh every StereoKit release using Input.HandOverride, to ensure consistency!_
```csharp
// These 25 joints were printed using code from a session with a real
// hand.
HandJoint[] joints = new HandJoint[] { new HandJoint(new Vec3(0.132f, -0.221f, -0.168f), new Quat(-0.445f, -0.392f, 0.653f, -0.472f), 0.021f), new HandJoint(new Vec3(0.132f, -0.221f, -0.168f), new Quat(-0.445f, -0.392f, 0.653f, -0.472f), 0.021f), new HandJoint(new Vec3(0.141f, -0.181f, -0.181f), new Quat(-0.342f, -0.449f, 0.618f, -0.548f), 0.014f), new HandJoint(new Vec3(0.139f, -0.151f, -0.193f), new Quat(-0.409f, -0.437f, 0.626f, -0.499f), 0.010f), new HandJoint(new Vec3(0.141f, -0.133f, -0.198f), new Quat(-0.409f, -0.437f, 0.626f, -0.499f), 0.010f), new HandJoint(new Vec3(0.124f, -0.229f, -0.172f), new Quat(0.135f, -0.428f, 0.885f, -0.125f), 0.024f), new HandJoint(new Vec3(0.103f, -0.184f, -0.209f), new Quat(0.176f, -0.530f, 0.774f, -0.299f), 0.013f), new HandJoint(new Vec3(0.078f, -0.153f, -0.225f), new Quat(0.173f, -0.645f, 0.658f, -0.349f), 0.010f), new HandJoint(new Vec3(0.061f, -0.135f, -0.228f), new Quat(-0.277f, 0.674f, -0.623f, 0.283f), 0.010f), new HandJoint(new Vec3(0.050f, -0.125f, -0.227f), new Quat(-0.277f, 0.674f, -0.623f, 0.283f), 0.010f), new HandJoint(new Vec3(0.119f, -0.235f, -0.172f), new Quat(0.147f, -0.399f, 0.847f, -0.318f), 0.024f), new HandJoint(new Vec3(0.088f, -0.200f, -0.211f), new Quat(0.282f, -0.603f, 0.697f, -0.268f), 0.012f), new HandJoint(new Vec3(0.056f, -0.169f, -0.216f), new Quat(-0.370f, 0.871f, -0.308f, 0.099f), 0.010f), new HandJoint(new Vec3(0.045f, -0.156f, -0.195f), new Quat(-0.463f, 0.884f, -0.022f, -0.066f), 0.010f), new HandJoint(new Vec3(0.047f, -0.155f, -0.178f), new Quat(-0.463f, 0.884f, -0.022f, -0.066f), 0.010f), new HandJoint(new Vec3(0.111f, -0.244f, -0.173f), new Quat(0.182f, -0.436f, 0.778f, -0.414f), 0.022f), new HandJoint(new Vec3(0.074f, -0.213f, -0.205f), new Quat(-0.353f, 0.622f, -0.656f, 0.244f), 0.011f), new HandJoint(new Vec3(0.046f, -0.189f, -0.204f), new Quat(-0.436f, 0.891f, -0.073f, -0.108f), 0.010f), new HandJoint(new Vec3(0.048f, -0.184f, -0.182f), new Quat(-0.451f, 0.811f, 0.264f, -0.263f), 0.010f), new HandJoint(new Vec3(0.061f, -0.188f, -0.168f), new Quat(-0.451f, 0.811f, 0.264f, -0.263f), 0.010f), new HandJoint(new Vec3(0.105f, -0.250f, -0.170f), new Quat(0.219f, -0.470f, 0.678f, -0.521f), 0.020f), new HandJoint(new Vec3(0.062f, -0.228f, -0.196f), new Quat(-0.444f, 0.610f, -0.623f, 0.206f), 0.010f), new HandJoint(new Vec3(0.044f, -0.215f, -0.192f), new Quat(-0.501f, 0.841f, -0.094f, -0.183f), 0.010f), new HandJoint(new Vec3(0.048f, -0.209f, -0.176f), new Quat(-0.521f, 0.682f, 0.251f, -0.448f), 0.010f), new HandJoint(new Vec3(0.061f, -0.207f, -0.168f), new Quat(-0.521f, 0.682f, 0.251f, -0.448f), 0.010f), new HandJoint(new Vec3(0.098f, -0.222f, -0.191f), new Quat(0.308f, -0.906f, 0.288f, -0.042f), 0.000f), new HandJoint(new Vec3(0.131f, -0.251f, -0.164f), new Quat(0.188f, -0.436f, 0.844f, -0.248f), 0.000f) };

Input.HandOverride(Handed.Right, joints);
```

# Working with Materials

Materials describe the visual appearance of everything on-screen, so having
a solid understanding of how they work is important to making a good
looking application! Fortunately, StereoKit comes with some great tools
built-in, and Materials can be a _lot_ of fun to work with!

## Using Materials

We've already seen that we can use a Material like this:
```csharp
Mesh.Sphere.Draw(Material.Default, Matrix.Identity);
```
This uses the primary default Material, which is a simple but
extremely fast and flexible Material. The default is great, but
not very interesting, it's just a white matte
surface! If we want it to look different, we'll have to change some
of the Material's parameters.

Before we can change the Material's parameters, I'd like to
point out an important fact! StereoKit does not draw objects
immediately when Draw is called: instead, it stores draw
information, and at the end of the frame it will sort, cull, and
batch everything, and _then_ draw it all at once! Since a Material
is a shared Asset, Meshes are drawn with the Material as it appears
at the end of the frame!

This means you **cannot** take one Material, modify it, draw,
modify it again, draw, and expect them to look different. Both
draw calls share the same Material Asset, and will look the same.
Instead, you _must_ make a new Material for each visually distinct
surface. Here's what that looks like:

### Material from Copy
```csharp
Material newMaterial;

void InitNewMaterial()
{
	// Start by just making a duplicate of the default! This creates a new
	// Material that we're free to change as much as we like.
	newMaterial = Material.Default.Copy();

	// Assign an image file as the primary color of the surface.
	newMaterial[MatParamName.DiffuseTex] = Tex.FromFile("floor.png");

	// Tint the whole thing greenish.
	newMaterial[MatParamName.ColorTint] = Color.HSV(0.3f, 0.4f, 1.0f);
}
void StepNewMaterial()
{
	Mesh.Sphere.Draw(newMaterial, Matrix.T(0,-3,0));
}
```
![Working with Material copies]({{site.screen_url}}/Materials_NewMaterial.jpg)
_It's uh... not the most glamorous material!_

Not all Materials will have the same parameters, and in fact,
parameters can vary wildly from Material to Material! This comes from
the Shader code that each Material has embedded at its core. The
Shader runs on the GPU, describes how each vertex is projected onto the
screen, and calculates the color of every pixel. Since each shader
program is different, each one has different parameters it works with!

While [`MatParamName`]({{site.url}}/Pages/Reference/MatParamName.html)
helps to codify and standardize common parameter names, it's always
best to be somewhat familiar with the Shader that the Material is
using.

For example, Material.Default uses [this Shader](https://github.com/StereoKit/StereoKit/blob/master/StereoKitC/shaders_builtin/shader_builtin_default.hlsl),
and you can see the parameters listed at the top:
```csharp
//--color:color = 1,1,1,1
//--tex_scale   = 1
//--diffuse     = white

float4    color;
float     tex_scale;
Texture2D diffuse : register(t0);
```
Shaders use data embedded in comments to assign default values to
material properties, the `//--` indicates this. So in this case,
`color` is a float4 (Vec4 or Color in C#), with a default value of
`1,1,1,1`, white. This maps to [`MatParamName.ColorTint`]({{site.url}}/Pages/Reference/MatParamName.html),
but you could also use the name directly:
`newMaterial["color"] = Color.HSV(0.3f, 0.2f, 1.0f);`.

Materials also have a few properties that aren't part of the Shader,
things like [depth testing]({{site.url}}/Pages/Reference/Material/DepthTest.html)/[writing]({{site.url}}/Pages/Reference/Material/DepthWrite.html),
[transparency]({{site.url}}/Pages/Reference/Material/Transparency.html),
[face culling]({{site.url}}/Pages/Reference/Material/FaceCull.html),
or [wireframe]({{site.url}}/Pages/Reference/Material/Wireframe.html).

### Material from Shader

You can also create a completely new Material directly from a Shader!
StereoKit does keep the default Shaders around in the [`Shader`]({{site.url}}/Pages/Reference/Shader.html)
class for this purpose, but you can also use Shader.FromFile to load a
pre-compiled shader file, and use that instead. More on that in the
[Shader guide (coming soon)]().
```csharp
Material shaderMaterial;

void InitShaderMaterial()
{
	// Instead of copying Material.Default, we're creating a completely new
	// Material directly from a Shader.
	shaderMaterial = new Material(Shader.Default);

	// Make it just slightly transparent
	shaderMaterial.Transparency = Transparency.Blend;
	shaderMaterial[MatParamName.ColorTint] = new Color(1, 1, 1, 0.9f);
}
void StepShaderMaterial()
{
	Mesh.Sphere.Draw(shaderMaterial, Matrix.T(0,2,0));
}
```
![Material from a Shader]({{site.screen_url}}/Materials_ShaderMaterial.jpg)
_It's a spooky circle now._
## Environment and Lighting

StereoKit's default lighting system is entirely based on environment
lighting! This can drastically affect how a material looks, so choosing the
right lighting can make a big difference in how your content looks. Here's
a simple white sphere again, but with a more complex lighting than the
default white room.

![Interesting lighting]({{site.screen_url}}/MaterialDefault.jpg)

You can change the environment lighting with a nice cubemap, check out the
[`Renderer.SkyLight`]({{site.url}}/Pages/Reference/Renderer/SkyLight.html)
property for a nice example of how to do this!

## Materials and Performance

Since Materials are responsible for drawing everything on the screen, they
have a big impact on GPU side performance! If you check your device's
performance monitor and see the GPU maxed out at 100% all the time, it's a
good moment to take a peek at how you're working with Materials.

The first rule is that fewer Materials means better GPU utilization. GPUs
don't like switching between Shaders or even Material parameters, so if you
can re-use a Material safely, you should! StereoKit does a great job of
batching draw calls together to reduce this switching, but this is only
effective at boosting performance if Materials are getting re-used.

The next rule is that simpler Shaders are faster. Material.Unlit is just
about the fastest Material you can have, followed closely by
Material.Default! Material.PBR looks great, but does a lot of work to look
good. It's very fast compared to many other PBR shaders, and quite suitable
even on mobile VR headsets, but if you don't need it, use something faster!

And lastly, small textures are faster than large ones. Textures get sampled
a lot during rendering, which means moving around lots of texture memory!
Remember that halving a texture's size can reduce memory by a factor of 4!

It often helps to just see how long a draw call takes! For this, I like to
use [RenderDoc](https://renderdoc.org/)'s timing feature. RenderDoc works
quite nicely with StereoKit's flatscreen mode, and while this isn't a
perfect representation of performance on mobile devices, it's a solid
reference point.

## A Look at the Defaults

StereoKit strives to cover the basics for you, and Materials are no
exception! You'll find a collection of Materials and Shaders that are
designed to be performant and good looking on mobile XR headsets, and
should cover the majority of use-cases. Here's a sampling, and check
the docs for each one to see what properties they support!

### [`Material.Default`]({{site.url}}/Pages/Reference/Default/Material.html)
![Material.Default preview]({{site.screen_url}}/MaterialDefault.jpg)

### [`Material.Unlit`]({{site.url}}/Pages/Reference/Default/MaterialUnlit.html)
![Material.Unlit preview]({{site.screen_url}}/MaterialUnlit.jpg)

### [`Material.PBR`]({{site.url}}/Pages/Reference/Default/MaterialPBR.html)
![Material.PBR preview]({{site.screen_url}}/MaterialPBR.jpg)

### [`Material.UI`]({{site.url}}/Pages/Reference/Default/MaterialUI.html)
![Material.UI preview]({{site.screen_url}}/MaterialUI.jpg)

### [`Material.UIBox`]({{site.url}}/Pages/Reference/Default/MaterialUIBox.html)
![Material.UIBox preview]({{site.screen_url}}/MaterialUIBox.jpg)

# Examples

### Checking for changes in application focus
```csharp
AppFocus lastFocus = AppFocus.Hidden;
void CheckFocus()
{
	if (lastFocus != SK.AppFocus)
	{
		lastFocus = SK.AppFocus;
		Log.Info($"App focus changed to: {lastFocus}");
	}
}
```
### General Usage

```csharp
// All these create bounds for a 1x1x1m cube around the origin!
Bounds bounds = new Bounds(Vec3.One);
bounds = Bounds.FromCorner(new Vec3(-0.5f, -0.5f, -0.5f), Vec3.One);
bounds = Bounds.FromCorners(
	new Vec3(-0.5f, -0.5f, -0.5f),
	new Vec3( 0.5f,  0.5f,  0.5f));

// Note that positions must be in a coordinate space relative to 
// the bounds!
if (bounds.Contains(new Vec3(0,0.25f,0)))
	Log.Info("Super easy to check if something's in it!");

// Casting a ray at a bounds is trivial too, again, ensure 
// coordinates are in the same space!
Ray ray = new Ray(Vec3.Up, -Vec3.Up);
if (bounds.Intersect(ray, out Vec3 at))
	Log.Info("Bounds intersection at " + at); // <0, 0.5f, 0>

// You can also scale a Bounds using the '*' operator overload, 
// this is really useful if you're working with the Bounds of a
// Model that you've scaled. It will scale the center as well as
// the size!
bounds = bounds * 0.5f;

// Scale the current bounds reference using 'Scale'
bounds.Scale(0.5f);

// Scale the bounds by a Vec3
bounds = bounds * new Vec3(1, 10, 0.5f);
```
```csharp
BtnState state = Input.Hand(Handed.Right).pinch;

// You can check a BtnState using bit-flag logic
if ((state & BtnState.Changed) > 0)
	Log.Info("Pinch state just changed!");

// Or you can check the same values with the extension methods, no
// bit flag logic :)
if (state.IsChanged())
	Log.Info("Pinch state just changed!");
```
```csharp
// You can create a color using Red, Green, Blue, Alpha values,
// but it's often a great recipe for making a bad color.
Color color = new Color(1,0,0,1); // Red

// Hue, Saturation, Value, Alpha is a more natural way of picking
// colors. The commentdocs have a list of important values for Hue,
// to make it a little easier to pick the hue you want.
color = Color.HSV(0, 1, 1, 1); // Red

// And there's a few static colors available if you need 'em.
color = Color.White;

// You can also implicitly convert Color to a Color32!
Color32 color32 = color;
```
### Creating color from hex values
```csharp
Color   hex128 = Color  .Hex(0xFF0000FF); // Opaque Red
Color32 hex32  = Color32.Hex(0x00FF00FF); // Opaque Green
```
```csharp
// Desaturating a color can be done quite nicely with the HSV
// functions
Color red      = new Color(1,0,0,1);
Vec3  colorHSV = red.ToHSV();
colorHSV.y *= 0.5f; // Drop saturation by half
Color desaturatedRed = Color.HSV(colorHSV, red.a);

// LAB color space is excellent for modifying perceived 
// brightness, or 'Lightness' of a color.
Color green    = new Color(0,1,0,1);
Vec3  colorLAB = green.ToLAB();
colorLAB.x *= 0.5f; // Drop lightness by half
Color darkGreen = Color.LAB(colorLAB, green.a);
```
```csharp
// You can create a color using Red, Green, Blue, Alpha values,
// but it's often a great recipe for making a bad color.
Color32 color = new Color32(255, 0, 0, 255); // Red

// Hue, Saturation, Value, Alpha is a more natural way of picking
// colors. You can use Color's HSV function, plus the implicit
// conversion to make a Color32!
color = Color.HSV(0, 1, 1, 1); // Red

// And there's a few static colors available if you need 'em.
color = Color32.White;
```
### Controller Debug Visualizer
This function shows a debug visualization of the current state of
the controller! It's not something you'd show to users, but it's
nice for just seeing how the API works, or as a temporary
visualization.
```csharp
void ShowController(Handed hand)
{
	Controller c = Input.Controller(hand);
	if (!c.IsTracked) return;

	Hierarchy.Push(c.pose.ToMatrix());
		// Pick the controller color based on trackin info state
		Color color = Color.Black;
		if (c.trackedPos == TrackState.Inferred) color.g = 0.5f;
		if (c.trackedPos == TrackState.Known)    color.g = 1;
		if (c.trackedRot == TrackState.Inferred) color.b = 0.5f;
		if (c.trackedRot == TrackState.Known)    color.b = 1;
		Default.MeshCube.Draw(Default.Material, Matrix.S(new Vec3(3, 3, 8) * U.cm), color);

		// Show button info on the back of the controller
		Hierarchy.Push(Matrix.TR(0,1.6f*U.cm,0, Quat.LookAt(Vec3.Zero, new Vec3(0,1,0), new Vec3(0,0,-1))));

			// Show the tracking states as text
			Text.Add(c.trackedPos==TrackState.Known?"(pos)":(c.trackedPos==TrackState.Inferred?"~pos~":"pos"), Matrix.TS(0,-0.03f,0, 0.25f));
			Text.Add(c.trackedRot==TrackState.Known?"(rot)":(c.trackedRot==TrackState.Inferred?"~rot~":"rot"), Matrix.TS(0,-0.02f,0, 0.25f));

			// Show the controller's buttons
			Text.Add(Input.ControllerMenuButton.IsActive()?"(menu)":"menu", Matrix.TS(0,-0.01f,0, 0.25f));
			Text.Add(c.IsX1Pressed?"(X1)":"X1", Matrix.TS(0,0.00f,0, 0.25f));
			Text.Add(c.IsX2Pressed?"(X2)":"X2", Matrix.TS(0,0.01f,0, 0.25f));

			// Show the analog stick's information
			Vec3 stickAt = new Vec3(0, 0.03f, 0);
			Lines.Add(stickAt, stickAt + c.stick.XY0*0.01f, Color.White, 0.001f);
			if (c.IsStickClicked) Text.Add("O", Matrix.TS(stickAt, 0.25f));

			// And show the trigger and grip buttons
			Default.MeshCube.Draw(Default.Material, Matrix.TS(0, -0.015f, -0.005f, new Vec3(0.01f, 0.04f, 0.01f)) * Matrix.TR(new Vec3(0,0.02f,0.03f), Quat.FromAngles(-45+c.trigger*40, 0,0) ));
			Default.MeshCube.Draw(Default.Material, Matrix.TS(0.0149f*(hand == Handed.Right?1:-1), 0, 0.015f, new Vec3(0.01f*(1-c.grip), 0.04f, 0.01f)));

		Hierarchy.Pop();
	Hierarchy.Pop();

	// And show the pointer
	Default.MeshCube.Draw(Default.Material, c.aim.ToMatrix(new Vec3(1,1,4) * U.cm), Color.HSV(0,0.5f,0.8f).ToLinear());
}
```
If you want to modify the default material, it's recommended to
copy it first!
```csharp
matDefault = Material.Default.Copy();
```
And here's what it looks like:
![Default Material example]({{site.screen_url}}/MaterialDefault.jpg)
Occlusion (R), Roughness (G), and Metal (B) are stored
respectively in the R, G and B channels of their texture.
Occlusion can be separated out into a different texture as per
the GLTF spec, so you do need to assign it separately from the
Metal texture.
```csharp
matPBR = Material.PBR.Copy();
matPBR[MatParamName.DiffuseTex  ] = Tex.FromFile("metal_plate_diff.jpg");
matPBR[MatParamName.MetalTex    ] = Tex.FromFile("metal_plate_metal.jpg", false);
matPBR[MatParamName.OcclusionTex] = Tex.FromFile("metal_plate_metal.jpg", false);
```
![PBR material example]({{site.screen_url}}/MaterialPBR.jpg)
This Material is basically the same as Default.Material, except it
also adds some glow to the surface near the user's fingers. It
works best on flat surfaces, and in StereoKit's design language,
can be used to indicate that something is interactive.
```csharp
matUI = Material.UI.Copy();
```
And here's what it looks like:
![UI Material example]({{site.screen_url}}/MaterialUI.jpg)
The UI Box material has 3 parameters to control how the box wires
are rendered. The initial size in meters is 'border_size', and
can grow by 'border_size_grow' meters based on distance to the
user's hand. That distance can be configured via the
'border_affect_radius' property of the shader, which is also in
meters.
```csharp
matUIBox = Material.UIBox.Copy();
matUIBox["border_size"]          = 0.005f;
matUIBox["border_size_grow"]     = 0.01f;
matUIBox["border_affect_radius"] = 0.2f;
```
![UI box material example]({{site.screen_url}}/MaterialUIBox.jpg)
```csharp
matUnlit = Material.Unlit.Copy();
matUnlit[MatParamName.DiffuseTex] = Tex.FromFile("floor.png");
```
![Unlit material example]({{site.screen_url}}/MaterialUnlit.jpg)
```csharp
SK.RemoveStepper(handMenu); 
```
### Basic layered hand menu

The HandMenuRadial is an `IStepper`, so it should be registered with
`StereoKitApp.AddStepper` so it can run by itself! It's recommended to
keep track of it anyway, so you can remove it when you're done with it
via `StereoKitApp.RemoveStepper`

The constructor uses a params style argument list that makes it easy and
clean to provide lists of items! This means you can assemble the whole
menu on a single 'line'. You can still pass arrays instead if you prefer
that!
```csharp
handMenu = SK.AddStepper(new HandMenuRadial(
	new HandRadialLayer("Root",
		new HandMenuItem("File",   null, null, "File"),
		new HandMenuItem("Edit",   null, null, "Edit"),
		new HandMenuItem("About",  null, () => Log.Info(SK.VersionName)),
		new HandMenuItem("Cancel", null, null)),
	new HandRadialLayer("File", 
		new HandMenuItem("New",   null, () => Log.Info("New")),
		new HandMenuItem("Open",  null, () => Log.Info("Open")),
		new HandMenuItem("Close", null, () => Log.Info("Close")),
		new HandMenuItem("Back",  null, null, HandMenuAction.Back)),
	new HandRadialLayer("Edit",
		new HandMenuItem("Copy",  null, () => Log.Info("Copy")),
		new HandMenuItem("Paste", null, () => Log.Info("Paste")),
		new HandMenuItem("Back", null, null, HandMenuAction.Back))));
```
```csharp
if (Input.EyesTracked.IsActive())
{
	// Intersect the eye Ray with a floor plane
	Plane plane = new Plane(Vec3.Zero, Vec3.Up);
	if (Input.Eyes.Ray.Intersect(plane, out Vec3 at))
	{
		Default.MeshSphere.Draw(Default.Material, Matrix.TS(at, .05f));
	}
}
```
Here's a small example of checking to see if a finger joint is inside
a box, and drawing an axis gizmo when it is!
```csharp
// A volume for checking inside of! 10cm on each side, at the origin
Bounds testArea = new Bounds(Vec3.One * 0.1f);

// This is a decent way to show we're working with both hands
for (int h = 0; h < (int)Handed.Max; h++)
{
	// Get the pose for the index fingertip
	Hand hand      = Input.Hand((Handed)h);
	Pose fingertip = hand[FingerId.Index, JointId.Tip].Pose;

	// Draw the fingertip pose axis if it's inside the volume
	if (testArea.Contains(fingertip.position))
		Lines.AddAxis(fingertip);
}
```
```csharp
Lines.Add(new LinePoint[]{ 
	new LinePoint(new Vec3( 0.1f, 0,     0), Color.White, 0.01f),
	new LinePoint(new Vec3( 0,    0.02f, 0), Color.Black, 0.005f),
	new LinePoint(new Vec3(-0.1f, 0,     0), Color.White, 0.01f),
});
```
```csharp
Lines.Add(new Vec3(0.1f,0,0), new Vec3(-0.1f,0,0), Color.White, Color.Black, 0.01f);
```
```csharp
Lines.Add(new Vec3(0.1f,0,0), new Vec3(-0.1f,0,0), Color.White, 0.01f);
```
```csharp
if (Time.Elapsedf > 0.017f)
	Log.Err("Oh no! Frame time (<~red>{0}<~clr>) has exceeded 17ms! There's no way we'll hit even 60 frames per second!", Time.Elapsedf);
```
Show everything that StereoKit logs!
```csharp
Log.Filter = LogLevel.Diagnostic;
```
Or, only show warnings and errors:
```csharp
Log.Filter = LogLevel.Warning;
```
```csharp
Log.Info("<~grn>{0:0.0}s<~clr> have elapsed since StereoKit start.", Time.Total);
```
Then you add the OnLog method into the log events like this in
your initialization code!
```csharp
Log.Subscribe(OnLog);
```
And in your Update loop, you can draw the window.
```csharp
LogWindow();
```
And that's it!
### An in-application log window
Here's an example of using the Log.Subscribe method to build a simple
logging window. This can be pretty handy to have around somewhere in
your application!

Here's the code for the window, and log tracking.
```csharp
static Pose         logPose = new Pose(0, -0.1f, 0.5f, Quat.LookDir(Vec3.Forward));
static List<string> logList = new List<string>();
static string       logText = "";
static void OnLog(LogLevel level, string text)
{
	if (logList.Count > 15)
		logList.RemoveAt(logList.Count - 1);
	logList.Insert(0, text.Length < 100 ? text : text.Substring(0,100)+"...\n");

	logText = "";
	for (int i = 0; i < logList.Count; i++)
		logText += logList[i];
}
static void LogWindow()
{
	UI.WindowBegin("Log", ref logPose, new Vec2(40, 0) * U.cm);
	UI.Text(logText);
	UI.WindowEnd();
}
```
```csharp
LogCallback onLog = (LogLevel level, string logText) 
	=> Console.WriteLine(logText);

Log.Subscribe(onLog);
```
...
```csharp
Log.Unsubscribe(onLog);
```
```csharp
Log.Warn("Warning! <~ylw>{0:0.0}s<~clr> have elapsed since StereoKit start!", Time.Total);
```
```csharp
Log.Write(LogLevel.Info, "<~grn>{0:0.0}s<~clr> have elapsed since StereoKit start.", Time.Total);
```
### Material parameter access
Material does have an array operator overload for setting
shader parameters really quickly! You can do this with strings
representing shader parameter names, or use the MatParamName
enum for compile safety.
```csharp
exampleMaterial[MatParamName.DiffuseTex] = gridTex;
exampleMaterial[MatParamName.TexScale  ] = 2.0f;
```
Here's setting FaceCull to Front, which is the opposite of the
default behavior. On a sphere, this is a little hard to see, but
you might notice here that the lighting is for the back side of
the sphere!
```csharp
matCull = Material.Default.Copy();
matCull.FaceCull = Cull.Front;
```
![FaceCull material example]({{site.screen_url}}/MaterialCull.jpg)
### Listing parameters in a Material
```csharp
// Iterate using a foreach
Log.Info("Builtin PBR Materials contain these parameters:");
foreach (MatParamInfo info in Material.PBR.GetAllParamInfo())
	Log.Info($"- {info.name} : {info.type}");

// Or with a normal for loop
Log.Info("Builtin Unlit Materials contain these parameters:");
for (int i=0; i<Material.Unlit.ParamCount; i+=1)
{
	MatParamInfo info = Material.Unlit.GetParamInfo(i);
	Log.Info($"- {info.name} : {info.type}");
}
```
### Additive Transparency
Here's an example material with additive transparency.
Transparent materials typically don't write to the depth buffer,
but this may vary from case to case. Note that the material's
alpha does not play any role in additive transparency! Instead,
you could make the material's tint darker.
```csharp
matAlphaAdd = Material.Default.Copy();
matAlphaAdd.Transparency = Transparency.Add;
matAlphaAdd.DepthWrite   = false;
```
![Additive transparency example]({{site.screen_url}}/MaterialAlphaAdd.jpg)
### Alpha Blending
Here's an example material with an alpha blend transparency.
Transparent materials typically don't write to the depth buffer,
but this may vary from case to case. Here we're setting the alpha
through the material's Tint value, but the diffuse texture's
alpha and the instance render color's alpha may also play a part
in the final alpha value.
```csharp
matAlphaBlend = Material.Default.Copy();
matAlphaBlend.Transparency = Transparency.Blend;
matAlphaBlend.DepthWrite   = false;
matAlphaBlend[MatParamName.ColorTint] = new Color(1, 1, 1, 0.75f);
```
![Alpha blend example]({{site.screen_url}}/MaterialAlphaBlend.jpg)
Here's creating a simple wireframe material!
```csharp
matWireframe = Material.Default.Copy();
matWireframe.Wireframe = true;
```
Which looks like this:
![Wireframe material example]({{site.screen_url}}/MaterialWireframe.jpg)
Drawing both a Mesh and a Model generated this way is reasonably simple,
here's a short example! For the Mesh, you'll need to create your own material,
we just loaded up the default Material here.
```csharp
// Call this code every Step

Matrix roundedCubeTransform = Matrix.T(-.5f, 0, 0);
roundedCubeMesh.Draw(Default.Material, roundedCubeTransform);

roundedCubeTransform = Matrix.T(.5f, 0, 0);
roundedCubeModel.Draw(roundedCubeTransform);
```
### Generating a Mesh and Model

![Procedural Geometry Demo]({{site.url}}/img/screenshots/ProceduralGeometry.jpg)

Here's a quick example of generating a mesh! You can store it in just a
Mesh, or you can attach it to a Model for easier rendering later on.
```csharp
// Do this in your initialization
Mesh  roundedCubeMesh  = Mesh.GenerateRoundedCube(Vec3.One * 0.4f, 0.05f);
Model roundedCubeModel = Model.FromMesh(roundedCubeMesh, Default.Material);
```
Drawing both a Mesh and a Model generated this way is reasonably simple,
here's a short example! For the Mesh, you'll need to create your own material,
we just loaded up the default Material here.
```csharp
// Call this code every Step

Matrix cubeTransform = Matrix.T(-.5f, -.5f, 0);
cubeMesh.Draw(Default.Material, cubeTransform);

cubeTransform = Matrix.T(.5f, -.5f, 0);
cubeModel.Draw(cubeTransform);
```
### UV and Face layout
Here's a test image that illustrates how this mesh's geometry is
laid out.
![Procedural Cube Mesh]({{site.screen_url}}/ProcGeoCube.jpg)
```csharp
meshCube = Mesh.GenerateCube(Vec3.One);
```
### Generating a Mesh and Model

![Procedural Geometry Demo]({{site.url}}/img/screenshots/ProceduralGeometry.jpg)

Here's a quick example of generating a mesh! You can store it in just a
Mesh, or you can attach it to a Model for easier rendering later on.
```csharp
Mesh  cubeMesh  = Mesh.GenerateCube(Vec3.One * 0.4f);
Model cubeModel = Model.FromMesh(cubeMesh, Default.Material);
```
### Generating a Mesh and Model

![Procedural Geometry Demo]({{site.url}}/img/screenshots/ProceduralGeometry.jpg)

Here's a quick example of generating a mesh! You can store it in just a
Mesh, or you can attach it to a Model for easier rendering later on.
```csharp
// Do this in your initialization
Mesh  cylinderMesh  = Mesh.GenerateCylinder(0.4f, 0.4f, Vec3.Up);
Model cylinderModel = Model.FromMesh(cylinderMesh, Default.Material);
```
Drawing both a Mesh and a Model generated this way is reasonably simple,
here's a short example! For the Mesh, you'll need to create your own material,
we just loaded up the default Material here.
```csharp
// Call this code every Step

Matrix cylinderTransform = Matrix.T(-.5f, 1, 0);
cylinderMesh.Draw(Default.Material, cylinderTransform);

cylinderTransform = Matrix.T(.5f, 1, 0);
cylinderModel.Draw(cylinderTransform);
```
### UV and Face layout
Here's a test image that illustrates how this mesh's geometry is
laid out.
![Procedural Cube Mesh]({{site.screen_url}}/ProcGeoCylinder.jpg)
```csharp
meshCylinder = Mesh.GenerateCylinder(1, 1, Vec3.Up);
```
### Generating a Mesh and Model

![Procedural Geometry Demo]({{site.url}}/img/screenshots/ProceduralGeometry.jpg)

Here's a quick example of generating a mesh! You can store it in just a
Mesh, or you can attach it to a Model for easier rendering later on.
```csharp
// Do this in your initialization
Mesh  planeMesh  = Mesh.GeneratePlane(Vec2.One*0.4f);
Model planeModel = Model.FromMesh(planeMesh, Default.Material);
```
### UV and Face layout
Here's a test image that illustrates how this mesh's geometry is
laid out.
![Procedural Cube Mesh]({{site.screen_url}}/ProcGeoPlane.jpg)
```csharp
meshPlane = Mesh.GeneratePlane(Vec2.One);
```
Drawing both a Mesh and a Model generated this way is reasonably simple,
here's a short example! For the Mesh, you'll need to create your own material,
we just loaded up the default Material here.
```csharp
Matrix planeTransform = Matrix.T(-.5f, -1, 0);
planeMesh.Draw(Default.Material, planeTransform);

planeTransform = Matrix.T(.5f, -1, 0);
planeModel.Draw(planeTransform);
```
### UV and Face layout
Here's a test image that illustrates how this mesh's geometry is
laid out.
![Procedural Cube Mesh]({{site.screen_url}}/ProcGeoRoundedCube.jpg)
```csharp
meshRoundedCube = Mesh.GenerateRoundedCube(Vec3.One, 0.05f);
```
### UV and Face layout
Here's a test image that illustrates how this mesh's geometry is
laid out.
![Procedural Cube Mesh]({{site.screen_url}}/ProcGeoSphere.jpg)
```csharp
meshSphere = Mesh.GenerateSphere(1);
```
Drawing both a Mesh and a Model generated this way is reasonably simple,
here's a short example! For the Mesh, you'll need to create your own material,
we just loaded up the default Material here.
```csharp
// Call this code every Step

Matrix sphereTransform = Matrix.T(-.5f, .5f, 0);
sphereMesh.Draw(Default.Material, sphereTransform);

sphereTransform = Matrix.T(.5f, .5f, 0);
sphereModel.Draw(sphereTransform);
```
### Generating a Mesh and Model

![Procedural Geometry Demo]({{site.url}}/img/screenshots/ProceduralGeometry.jpg)

Here's a quick example of generating a mesh! You can store it in just a
Mesh, or you can attach it to a Model for easier rendering later on.
```csharp
// Do this in your initialization
Mesh  sphereMesh  = Mesh.GenerateSphere(0.4f);
Model sphereModel = Model.FromMesh(sphereMesh, Default.Material);
```
### Procedurally generating a wavy grid

![Wavy Grid]({{site.url}}/img/screenshots/ProceduralGrid.jpg)

Here, we'll generate a grid mesh using Mesh.SetVerts and Mesh.SetInds! This
is a common example of creating a grid using code, we're using a sin wave
to make it more visually interesting, but you could also substitute this for
something like sampling a heightmap, or a more interesting mathematical
formula!

Note: x+y*gridSize is the formula for 2D (x,y) access of a 1D array that represents
a grid.
```csharp
const int   gridSize = 8;
const float gridMaxF = gridSize-1;
Vertex[] verts = new Vertex[gridSize*gridSize];
uint  [] inds  = new uint  [gridSize*gridSize*6];

for (int y = 0; y < gridSize; y++) {
for (int x = 0; x < gridSize; x++) {
	// Create a vertex on a grid, centered about the origin. The dimensions extends
	// from -0.5 to +0.5 on the X and Z axes. The Y value is then sampled from 
	// a sin wave using the x and y values.
	//
	// The normal of the vertex is then calculated from the derivative of the Y 
	// value!
	verts[x+y*gridSize] = new Vertex(
		new Vec3(
			x/gridMaxF-0.5f, 
			SKMath.Sin((x+y) * 0.7f)*0.1f, 
			y/gridMaxF-0.5f),
		new Vec3(
			-SKMath.Cos((x+y) * 0.7f), 
			1, 
			-SKMath.Cos((x+y) * 0.7f)).Normalized,
		new Vec2(
			x / gridMaxF,
			y / gridMaxF));

	// Create triangle face indices from the current vertex, and the vertices
	// on the next row and column! Since there is no 'next' row and column on
	// the last row and column, we guard this with a check for size-1.
	if (x<gridSize-1 && y<gridSize-1)
	{
		int ind = (x+y*gridSize)*6;
		inds[ind  ] = (uint)((x+1)+(y+1)*gridSize);
		inds[ind+1] = (uint)((x+1)+(y  )*gridSize);
		inds[ind+2] = (uint)((x  )+(y+1)*gridSize);

		inds[ind+3] = (uint)((x  )+(y+1)*gridSize);
		inds[ind+4] = (uint)((x+1)+(y  )*gridSize);
		inds[ind+5] = (uint)((x  )+(y  )*gridSize);
	}
} }
demoProcMesh = new Mesh();
demoProcMesh.SetVerts(verts);
demoProcMesh.SetInds (inds);
```
### Choosing a microphone device
While generally you'll prefer to use the default device, it can be
nice to allow users to pick which mic they're using! This is
especially important on PC, where users may have complicated or
interesting setups.

![Microphone device selection window]({{site.screen_url}}/MicrophoneSelector.jpg)

This sample is a very simple window that allows users to start
recording with a device other than the default. NOTE: this example
is designed with the assumption that Microphone.Start() has been
called already.
```csharp
Pose     micSelectPose   = new Pose(0.5f, 0, -0.5f, Quat.LookDir(-1, 0, 1));
string[] micDevices      = null;
string   micDeviceActive = null;
void ShowMicDeviceWindow()
{
	// Let the user choose a microphone device
	UI.WindowBegin("Available Microphones:", ref micSelectPose);

	// User may plug or unplug a mic device, so it's nice to be able to
	// refresh this list.
	if (UI.Button("Refresh") || micDevices == null)
		micDevices = Microphone.GetDevices();
	UI.HSeparator();

	// Display the list of potential microphones. Some systems may only
	// have the default (null) device available.
	Vec2 size = V.XY(0.25f, UI.LineHeight);
	if (UI.Radio("Default", micDeviceActive == null, size))
	{
		micDeviceActive = null;
		Microphone.Start(micDeviceActive);
	}
	foreach (string device in micDevices)
	{
		if (UI.Radio(device, micDeviceActive == device, size))
		{
			micDeviceActive = device;
			Microphone.Start(micDeviceActive);
		}
	}

	UI.WindowEnd();
}
```
### Getting streaming sound intensity
This example shows how to read data from a Sound stream such as the
microphone! In this case, we're just finding the average 'intensity'
of the audio, and returning it as a value approximately between 0 and 1.
Microphone.Start() should be called before this example :)
```csharp
float[] micBuffer    = new float[0];
float   micIntensity = 0;
float GetMicIntensity()
{
	if (!Microphone.IsRecording) return 0;

	// Ensure our buffer of samples is large enough to contain all the
	// data the mic has ready for us this frame
	if (Microphone.Sound.UnreadSamples > micBuffer.Length)
		micBuffer = new float[Microphone.Sound.UnreadSamples];

	// Read data from the microphone stream into our buffer, and track 
	// how much was actually read. Since the mic data collection runs in
	// a separate thread, this will often be a little inconsistent. Some
	// frames will have nothing ready, and others may have a lot!
	int samples = Microphone.Sound.ReadSamples(ref micBuffer);

	// This is a cumulative moving average over the last 1000 samples! We
	// Abs() the samples since audio waveforms are half negative.
	for (int i = 0; i < samples; i++)
		micIntensity = (micIntensity*999.0f + Math.Abs(micBuffer[i]))/1000.0f;

	return micIntensity;
}
```
### Loading an animated Model
Here, we're loading a Model that we know has the animations "Idle"
and "Jump". This sample shows some options, but only a single call
to PlayAnim is necessary to start an animation.
```csharp
Model model = Model.FromFile("Cosmonaut.glb");

// You can look at the model's animations:
foreach (Anim anim in model.Anims)
	Log.Info($"Animation: {anim.Name} {anim.Duration}s");

// You can play an animation like this
model.PlayAnim("Jump", AnimMode.Once);

// Or you can find and store the animations in advance
Anim jumpAnim = model.FindAnim("Idle");
if (jumpAnim != null)
	model.PlayAnim(jumpAnim, AnimMode.Loop);
```
### Assembling a Model
While normally you'll load Models from file, you can also assemble
them yourself procedurally! This example shows assembling a simple
hierarchy of visual and empty nodes.
```csharp
Model model = new Model();
model
	.AddNode ("Root",    Matrix.S(0.2f), Mesh.Cube, Material.Default)
	.AddChild("Sub",     Matrix.TR (V.XYZ(0.5f, 0, 0), Quat.FromAngles(0, 0, 45)), Mesh.Cube, Material.Default)
	.AddChild("Surface", Matrix.TRS(V.XYZ(0.5f, 0, 0), Quat.LookDir(V.XYZ(1,0,0)), V.XYZ(1,1,1)));

ModelNode surfaceNode = model.FindNode("Surface");

surfaceNode.AddChild("UnitX", Matrix.T(Vec3.UnitX));
surfaceNode.AddChild("UnitY", Matrix.T(Vec3.UnitY));
surfaceNode.AddChild("UnitZ", Matrix.T(Vec3.UnitZ));
```
### Animation progress bar
A really simple progress bar visualization for the Model's active
animation.

![Model with progress bar]({{site.screen_url}}/AnimProgress.jpg)
```csharp
model.Draw(Matrix.Identity);

Hierarchy.Push(Matrix.T(0.5f, 1, -.25f));

// This is a pair of green lines that show the current progress through
// the animation.
float progress = model.AnimCompletion;
Lines.Add(V.XY0(0, 0), V.XY0(-progress, 0),  new Color(0,1,0,1.0f), 2*U.cm);
Lines.Add(V.XY0(-progress, 0), V.XY0(-1, 0), new Color(0,1,0,0.2f), 2*U.cm);

// These are some labels for the progress bar that tell us more about
// the active animation.
Text.Add($"{model.ActiveAnim.Name} : {model.AnimMode}", Matrix.TS(0, -2*U.cm, 0, 3),        TextAlign.TopLeft);
Text.Add($"{model.AnimTime:F1}s",                       Matrix.TS(-progress, 2*U.cm, 0, 3), TextAlign.BottomCenter);

Hierarchy.Pop();
```
### Recursive depth first node traversal
Recursive depth first traversal is a little simpler to implement as
long as you don't mind some recursion :)
This would be called like: `RecursiveTraversal(model.RootNode);`
```csharp
static void RecursiveTraversal(ModelNode node, int depth = 0)
{
	string tabs = new string(' ', depth*2);
	while (node != null)
	{
		Log.Info(tabs + node.Name);
		RecursiveTraversal(node.Child, depth + 1);
		node = node.Sibling;
	}
}
```
### An Interactive Model

![A grabbable GLTF Model using UI.Handle]({{site.screen_url}}/HandleBox.jpg)

If you want to grab a Model and move it around, then you can use a
`UI.Handle` to do it! Here's an example of loading a GLTF from file,
and using its information to create a Handle and a UI 'cage' box that
indicates an interactive element.

```csharp
Model model      = Model.FromFile("DamagedHelmet.gltf");
Pose  handlePose = new Pose(0,0,0, Quat.Identity);
float scale      = .15f;

public void Step() {
	UI.HandleBegin("Model Handle", ref handlePose, model.Bounds*scale);

	model.Draw(Matrix.S(scale));
	Mesh.Cube.Draw(Material.UIBox, Matrix.TS(model.Bounds.center*scale, model.Bounds.dimensions*scale));

	UI.HandleEnd();
}
```
### Simple iteration
Walking through the Model's list of nodes is pretty
straightforward! This will touch every ModelNode in the Model,
in the order they were defined, regardless of hierarchy position
or contents.
```csharp
Log.Info("Iterate nodes:");
foreach (ModelNode node in model.Nodes)
	Log.Info("  "+ node.Name);
```
### Collision Tagged Nodes
One particularly practical example of tagging your ModelNode names
would be to set up collision information for your Model. If, for
example, you have a low resolution mesh designed specifically for
fast collision detection, you can tag your non-solid nodes as
"[Intangible]", and your collider nodes as "[Invisible]":
```csharp
foreach (ModelNode node in model.Nodes)
{
	node.Solid   = node.Name.Contains("[Intangible]") == false;
	node.Visible = node.Name.Contains("[Invisible]")  == false;
}
```
### Non-recursive depth first node traversal
If you need to walk through a Model's node hierarchy, this is a method
of doing this without recursion! You essentially do this by walking the
tree down (Child) and to the right (Sibling), and if neither is present,
then walking back up (Parent) until it can keep going right (Sibling)
and then down (Child) again.
```csharp
static void DepthFirstTraversal(Model model)
{
	ModelNode node  = model.RootNode;
	int       depth = 0;
	while (node != null)
	{
		string tabs = new string(' ', depth*2);
		Log.Info(tabs + node.Name);

		if      (node.Child   != null) { node = node.Child; depth++; }
		else if (node.Sibling != null)   node = node.Sibling;
		else {
			while (node != null)
			{
				if (node.Sibling != null) {
					node = node.Sibling;
					break;
				}
				depth--;
				node = node.Parent;
			}
		}
	}
}
```
### Simple iteration of visual nodes
This will iterate through every ModelNode in the Model with visual
data attached to it!
```csharp
Log.Info("Iterate visuals:");
foreach (ModelNode node in model.Visuals)
	Log.Info("  "+ node.Name);
```
### Counting the Vertices and Triangles in a Model

Model.Visuals are always guaranteed to have a Mesh, so no need to
null check there, and VertCount and IndCount are available even if
Mesh.KeepData is false!
```csharp
int vertCount = 0;
int triCount  = 0;

foreach (ModelNode node in model.Visuals)
{
	Mesh mesh = node.Mesh;
	vertCount += mesh.VertCount;
	triCount  += mesh.IndCount / 3;
}
Log.Info($"Model stats: {vertCount} vertices, {triCount} triangles");
```
### Tagged Nodes
You can search through Visuals and Nodes for nodes with some sort
of tag in their names. Since these names are from your modeling
software, this can allow for some level of designer configuration
that can be specific to your project.
```csharp
var nodes = model.Visuals
	.Where(n => n.Name.Contains("[Wire]"));
foreach (ModelNode node in nodes)
{
	node.Material = node.Material.Copy();
	node.Material.Wireframe = true;
}
```
```csharp
model.AddSubset(
	Mesh   .GenerateSphere(1),
	Default.Material,
	Matrix .T(0,1,0));
```
### Copying assets
Modifying an asset will affect everything that uses that asset!
Often you'll want to copy an asset before modifying it, to
ensure other parts of your application look the same. In
particular, modifying default assets is not a good idea, unless
you _do_ want to modify the defaults globally.
```csharp
Model model1 = new Model(Mesh.Sphere, Material.Default);
model1.RootNode.LocalTransform = Matrix.S(0.1f);

Material mat = Material.Default.Copy();
mat[MatParamName.ColorTint] = new Color(1,0,0,1);
Model model2 = model1.Copy();
model2.RootNode.Material = mat;
```
```csharp
Model model = new Model();
model.AddNode("Cube",
	Matrix .Identity,
	Mesh   .GenerateCube(Vec3.One),
	Default.Material);
```
```csharp
for (int i = 0; i < model.SubsetCount; i++)
{
	// GetMaterial will often returned a shared resource, so 
	// copy it if you don't wish to change all assets that 
	// share it.
	Material mat = model.GetMaterial(i).Copy();
	mat[MatParamName.ColorTint] = Color.HSV(0, 1, 1);
	model.SetMaterial(i, mat);
}
```
Once you have the filename, it's simply a matter of loading it
from file. This is an example of async loading a model, and
calculating a scale value that ensures the model is a reasonable
size.
```csharp
private void OnLoadModel(string filename)
{
	model      = Model.FromFile(filename);
	modelTask  = Assets.CurrentTask;
	modelScale = 1 / model.Bounds.dimensions.Magnitude;
	if (model.Anims.Count > 0)
		model.PlayAnim(model.Anims[0], AnimMode.Loop);
}
```
### Opening a Model
This is a simple button that will open a 3D model selection
file picker, and make a call to OnLoadModel after a file has
been successfully picked!
```csharp
if (UI.Button("Open Model") && !Platform.FilePickerVisible) {
	Platform.FilePicker(PickerMode.Open, OnLoadModel, null, Assets.ModelFormats);
}
```
### Read Custom Files
```csharp
Platform.FilePicker(PickerMode.Open, file => {
	// On some platforms (like UWP), you may encounter permission
	// issues when trying to read or write to an arbitrary file.
	//
	// StereoKit's `Platform.FilePicker` and `Platform.ReadFile`
	// work together to avoid this permission issue, where the
	// FilePicker will grant permission to the ReadFile method.
	// C#'s built-in `File.ReadAllText` would fail on UWP here.
	if (Platform.ReadFile(file, out string text))
		Log.Info(text);
}, null, ".txt");
```
### Write Custom Files
```csharp
Platform.FilePicker(PickerMode.Save, file => {
	// On some platforms (like UWP), you may encounter permission
	// issues when trying to read or write to an arbitrary file.
	//
	// StereoKit's `Platform.FilePicker` and `Platform.WriteFile`
	// work together to avoid this permission issue, where the
	// FilePicker will grant permission to the WriteFile method.
	// C#'s built-in `File.WriteAllText` would fail on UWP here.
	Platform.WriteFile(file, "Text for the file.\n- Thanks!");
}, null, ".txt");
```
### Toggling the projection mode
Only in flatscreen apps, there is the option to change the main
camera's projection mode between perspective and orthographic.
```csharp
if (SK.ActiveDisplayMode == DisplayMode.Flatscreen &&
	Input.Key(Key.P).IsJustActive())
{
	Renderer.Projection = Renderer.Projection == Projection.Perspective
		? Projection.Ortho
		: Projection.Perspective;
}
```
Quat.LookAt and LookDir are probably one of the easiest ways to
work with quaternions in StereoKit! They're handy functions to
have a good understanding of. Here's an example of how you might
use them.
```csharp
// Draw a box that always rotates to face the user
Vec3 boxPos = new Vec3(1,0,1);
Quat boxRot = Quat.LookAt(boxPos, Input.Head.position);
Mesh.Cube.Draw(Material.Default, Matrix.TR(boxPos, boxRot));

// Make a Window that faces a user that enters the app looking
// Forward.
Pose winPose = new Pose(0,0,-0.5f, Quat.LookDir(0,0,1));
UI.WindowBegin("Posed Window", ref winPose);
UI.WindowEnd();

```
### Ray Mesh Intersection
Here's an example of casting a Ray at a mesh someplace in world space,
transforming it into model space, calculating the intersection point,
and displaying it back in world space.

![Ray Mesh Intersection]({{site.url}}/img/screenshots/RayMeshIntersect.jpg)

```csharp
Mesh sphereMesh = Default.MeshSphere;
Mesh boxMesh    = Mesh.GenerateRoundedCube(Vec3.One*0.2f, 0.05f);
Pose boxPose    = new Pose(0,     0,     -0.5f,  Quat.Identity);
Pose castPose   = new Pose(0.25f, 0.21f, -0.36f, Quat.Identity);

public void Update()
{
	// Draw our setup, and make the visuals grab/moveable!
	UI.Handle("Box",  ref boxPose,  boxMesh.Bounds);
	UI.Handle("Cast", ref castPose, sphereMesh.Bounds*0.03f);
	boxMesh   .Draw(Default.MaterialUI, boxPose .ToMatrix());
	sphereMesh.Draw(Default.MaterialUI, castPose.ToMatrix(0.03f));
	Lines.Add(castPose.position, boxPose.position, Color.White, 0.005f);

	// Create a ray that's in the Mesh's model space
	Matrix transform = boxPose.ToMatrix();
	Ray    ray       = transform
		.Inverse
		.Transform(Ray.FromTo(castPose.position, boxPose.position));

	// Draw a sphere at the intersection point, if the ray intersects 
	// with the mesh.
	if (ray.Intersect(boxMesh, out Ray at, out uint index))
	{
		sphereMesh.Draw(Default.Material, Matrix.TS(transform.Transform(at.position), 0.01f));
		if (boxMesh.GetTriangle(index, out Vertex a, out Vertex b, out Vertex c))
		{
			Vec3 aPt = transform.Transform(a.pos);
			Vec3 bPt = transform.Transform(b.pos);
			Vec3 cPt = transform.Transform(c.pos);
			Lines.Add(aPt, bPt, new Color32(0,255,0,255), 0.005f);
			Lines.Add(bPt, cPt, new Color32(0,255,0,255), 0.005f);
			Lines.Add(cPt, aPt, new Color32(0,255,0,255), 0.005f);
		}
	}
}
```
### Setting lighting to an equirect cubemap
Changing the environment's lighting based on an image is a really
great way to instantly get a particular feel to your scene! A neat
place to find compatible equirectangular images for this is
[Poly Haven](https://polyhaven.com/hdris)
```csharp
Renderer.SkyTex   = Tex.FromCubemapEquirectangular("old_depot.hdr", out SphericalHarmonics lighting);
Renderer.SkyLight = lighting;
```
And here's what it looks like applied to the default Material!
![Default Material example]({{site.screen_url}}/MaterialDefault.jpg)
### Basic usage
```csharp
Sound sound = Sound.FromFile("BlipNoise.wav");
sound.Play(Vec3.Zero);
```
### Generating a sound via samples
Making a procedural sound is pretty straightforward! Here's
an example of building a 500ms sound from two frequencies of
sin wave.
```csharp
float[] samples = new float[(int)(48000*0.5f)];
for (int i = 0; i < samples.Length; i++)
{
	float t = i/48000.0f;
	float band1 = SKMath.Sin(t * 523.25f * SKMath.Tau); // a 'C' tone
	float band2 = SKMath.Sin(t * 659.25f * SKMath.Tau); // an 'E' tone
	const float volume = 0.1f;
	samples[i] = (band1 * 0.6f + band2 * 0.4f) * volume;
}
Sound sampleSound = Sound.FromSamples(samples);
sampleSound.Play(Vec3.Zero);
```
### Generating a sound via generator
Making a procedural sound is pretty straightforward! Here's
an example of building a 500ms sound from two frequencies of
sin wave.
```csharp
Sound genSound = Sound.Generate((t) =>
{
	float band1 = SKMath.Sin(t * 523.25f * SKMath.Tau); // a 'C' tone
	float band2 = SKMath.Sin(t * 659.25f * SKMath.Tau); // an 'E' tone
	const float volume = 0.1f;
	return (band1*0.6f + band2*0.4f) * volume;
}, 0.5f);
genSound.Play(Vec3.Zero);
```
### Creating a texture procedurally
It's pretty easy to create an array of colors, and
just pass that into an empty texture! Here, we're
building a simple grid texture, like so:

![Procedural Texture]({{site.url}}/img/screenshots/ProceduralTexture.jpg)

You can call SetTexture as many times as you like! If
you're calling it frequently, you may want to keep
the width and height consistent to prevent from creating
new texture objects. Use TexType.ImageNomips to prevent
StereoKit from calculating mip-maps, which can be costly,
especially when done frequently.
```csharp
// Create an empty texture! This is TextType.Image, and 
// an RGBA 32 bit color format.
Tex gridTex = new Tex();

// Use point sampling to ensure that the grid lines are
// crisp and sharp, not blended with the pixels around it.
gridTex.SampleMode = TexSample.Point;

// Allocate memory for the pixels we'll fill in, powers
// of two are always best for textures, since this makes
// things like generating mip-maps easier.
int width  = 128;
int height = 128;
Color32[] colors = new Color32[width*height];

// Create a color for the base of the grid, and the
// lines of the grid
Color32 baseColor    = Color.HSV(0.6f,0.1f,0.25f);
Color32 lineColor    = Color.HSV(0.6f,0.05f,1);
Color32 subLineColor = Color.HSV(0.6f,0.05f,.6f);

// Loop through each pixel
for (int y = 0; y < height; y++) {
for (int x = 0; x < width;  x++) {
	// If the pixel's x or y value is a multiple of 64, or 
	// if it's adjacent to a multiple of 128, then we 
	// choose the line color! Otherwise, we use the base.
	if (x % 128 == 0 || (x+1)%128 == 0 || (x-1)%128 == 0 ||
		y % 128 == 0 || (y+1)%128 == 0 || (y-1)%128 == 0)
		colors[x+y*width] = lineColor;
	else if (x % 64 == 0 || y % 64 == 0)
		colors[x+y*width] = subLineColor;
	else
		colors[x+y*width] = baseColor;
} }

// Put the pixel information into the texture
gridTex.SetColors(width, height, colors);
```
Then it's pretty trivial to just draw some text on the screen! Just call
Text.Add on update. If you don't have a TextStyle available, calling it
without one will just fall back on the default style.
```csharp
// Text with an explicit text style
Text.Add(
	"Here's\nSome\nMulti-line\nText!!", 
	Matrix.TR(new Vec3(0.1f, 0, 0), Quat.LookDir(0, 0, 1)),
	style);
// Text using the default text style
Text.Add(
	"Here's\nSome\nMulti-line\nText!!", 
	Matrix.TR(new Vec3(-0.1f, 0, 0), Quat.LookDir(0, 0, 1)));
```
In initialization, we can create the style from a font, a size,
and a base color. Overloads for MakeStyle can allow you to
override the default font shader, or provide a specific Material.
```csharp
style = Text.MakeStyle(
	Font.FromFile("C:/Windows/Fonts/times.ttf") ?? Default.Font, 
	2 * U.cm,
	Color.HSV(0.55f, 0.62f, 0.93f));
```
### Drawing text with and without a TextStyle
![Basic text]({{site.url}}/img/screenshots/BasicText.jpg)
We can use a TextStyle object to control how text gets displayed!
```csharp
TextStyle style;
```
### A simple button

![A window with a button]({{site.screen_url}}/UI/ButtonWindow.jpg)

This is a complete window with a simple button on it! `UI.Button`
returns true only for the very first frame the button is pressed, so
using the `if(UI.Button())` pattern works very well for executing
code on button press!

```csharp
Pose windowPoseButton = new Pose(0, 0, 0, Quat.Identity);
void ShowWindowButton()
{
	UI.WindowBegin("Window Button", ref windowPoseButton);

	if (UI.Button("Press me!"))
		Log.Info("Button was pressed.");

	UI.WindowEnd();
}
```
### Separating UI Visually

![A window with text and a separator]({{site.screen_url}}/UI/SeparatorWindow.jpg)

A separator is a simple visual element that fills the window
horizontally. It's nothing complicated, but can help create visual
association between groups of UI elements.

```csharp
Pose windowPoseSeparator = new Pose(.6f, 0, 0, Quat.Identity);
void ShowWindowSeparator()
{
	UI.WindowBegin("Window Separator", ref windowPoseSeparator, UIWin.Body);

	UI.Label("Content Header");
	UI.HSeparator();
	UI.Text("A separator can go a long way towards making your content "
	      + "easier to look at!", TextAlign.TopCenter);

	UI.WindowEnd();
}
```
### Horizontal Sliders

![A window with a slider]({{site.screen_url}}/UI/SliderWindow.jpg)

A slider will slide between two values at increments. The function
requires a reference to a float variable where the slider's state is
stored. This allows you to manage the state yourself, and it's
completely valid for you to change the slider state separately, the
UI element will update to match.

Note that `UI.HSlider` returns true _only_ when the slider state has
changed, and does _not_ return the current state.

```csharp
Pose  windowPoseSlider = new Pose(.9f, 0, 0, Quat.Identity);
float sliderState      = 0.5f;
void ShowWindowSlider()
{
	UI.WindowBegin("Window Slider", ref windowPoseSlider);

	if (UI.HSlider("Slider", ref sliderState, 0, 1, 0.1f))
		Log.Info($"Slider value just changed: {sliderState}");

	UI.WindowEnd();
}
```
### Text Input

![A window with a text input]({{site.screen_url}}/UI/InputWindow.jpg)

The `UI.Input` element allows users to enter text. Upon selecting the
element, a virtual keyboard will appear on platforms that provide
one.  The function requires a reference to a string variable where
the input's state is stored. This allows you to manage the state
yourself, and it's completely valid for you to change the input state
separately, the UI element will update to match.

`UI.Input` will return true on frames where the text has _just_
changed.

```csharp
Pose   windowPoseInput = new Pose(1.2f, 0, 0, Quat.Identity);
string inputState      = "Initial text";
void ShowWindowInput()
{
	UI.WindowBegin("Window Input", ref windowPoseInput);

	// Add a small label in front of it on the same line
	UI.Label("Text:");
	UI.SameLine();
	if (UI.Input("Text", ref inputState))
		Log.Info($"Input text just changed");

	UI.WindowEnd();
}
```
### Checking UI element status
It can sometimes be nice to know how the user is interacting with a
particular UI element! The UI.LastElementX functions can be used to
query a bit of this information, but only for _the most recent_ UI
element that **uses an id**!

![A window containing the status of a UI element]({{site.screen_url}}/UI/LastElementAPI.jpg)

So in this example, we're querying the information for the "Slider"
UI element. Note that UI.Text does NOT use an id, which is why this
works.
```csharp
UI.WindowBegin("Last Element API", ref windowPose);

UI.HSlider("Slider", ref sliderVal, 0, 1, 0.1f, 0, UIConfirm.Pinch);
UI.Text("Element Info:", TextAlign.TopCenter);
if (UI.LastElementHandUsed(Handed.Left ).IsActive()) UI.Label("Left");
if (UI.LastElementHandUsed(Handed.Right).IsActive()) UI.Label("Right");
if (UI.LastElementFocused               .IsActive()) UI.Label("Focused");
if (UI.LastElementActive                .IsActive()) UI.Label("Active");

UI.WindowEnd();
```
### Radio button group

![A window with radio buttons]({{site.screen_url}}/UI/RadioWindow.jpg)

Radio buttons are a variety of Toggle button that behaves in a manner
more conducive to radio group style behavior. This is an example of
how to implement a small radio button group.

```csharp
Pose windowPoseRadio = new Pose(1.5f, 0, 0, Quat.Identity);
int  radioState      = 1;
void ShowWindowRadio()
{
	UI.WindowBegin("Window Radio", ref windowPoseRadio);

	if (UI.Radio("Option 1", radioState == 1)) radioState = 1;
	if (UI.Radio("Option 2", radioState == 2)) radioState = 2;
	if (UI.Radio("Option 3", radioState == 3)) radioState = 3;

	UI.WindowEnd();
}
```
### A toggle button

![A window with a toggle]({{site.screen_url}}/UI/ToggleWindow.jpg)

Toggle buttons swap between true and false when you press them! The
function requires a reference to a bool variable where the toggle's
state is stored. This allows you to manage the state yourself, and
it's completely valid for you to change the toggle state separately,
the UI element will update to match.

Note that `UI.Toggle` returns true _only_ when the toggle state has
changed, and does _not_ return the current state.

```csharp
Pose windowPoseToggle = new Pose(.3f, 0, 0, Quat.Identity);
bool toggleState      = true;
void ShowWindowToggle()
{
	UI.WindowBegin("Window Toggle", ref windowPoseToggle);

	if (UI.Toggle("Toggle me!", ref toggleState))
		Log.Info("Toggle just changed.");
	if (toggleState) UI.Label("Toggled On");
	else             UI.Label("Toggled Off");

	UI.WindowEnd();
}
```
This code will draw an axis at the index finger's location when
the user pinches while inside a VolumeAt.

![UI.InteractVolume]({{site.screen_url}}/InteractVolume.jpg)

```csharp
// Draw a transparent volume so the user can see this space
Vec3  volumeAt   = new Vec3(0,0.2f,-0.4f);
float volumeSize = 0.2f;
Default.MeshCube.Draw(Default.MaterialUIBox, Matrix.TS(volumeAt, volumeSize));

BtnState volumeState = UI.VolumeAt("Volume", new Bounds(volumeAt, Vec3.One*volumeSize), UIConfirm.Pinch, out Handed hand);
if (volumeState != BtnState.Inactive)
{
	// If it just changed interaction state, make it jump in size
	float scale = volumeState.IsChanged()
		? 0.1f
		: 0.05f;
	Lines.AddAxis(Input.Hand(hand)[FingerId.Index, JointId.Tip].Pose, scale);
}
```
```csharp
Vec2 point = new Vec2(1, 0);
float angle0 = point.Angle();

point = new Vec2(0, 1);
float angle90 = point.Angle();

point = new Vec2(-1, 0);
float angle180 = point.Angle();

point = new Vec2(0, -1);
float angle270 = point.Angle();
```
```csharp
Vec2 directionA = new Vec2( 1, 1);
Vec2 directionB = new Vec2(-1, 1);
float angle90 = Vec2.AngleBetween(directionA, directionB);

directionA = new Vec2(1, 1);
directionB = new Vec2(0,-2);
float angleNegative135 = Vec2.AngleBetween(directionA, directionB);
```
### Distance between two points

Distance does use a Sqrt call, so only use it if you definitely
need the actual distance. Otherwise, consider DistanceSq.
```csharp
Vec3  pointA   = new Vec3(3,2,5);
Vec3  pointB   = new Vec3(3,2,8);
float distance = Vec3.Distance(pointA, pointB);
```
```csharp
Vec3 pointA = new Vec3(3, 2, 5);
Vec3 pointB = new Vec3(3, 2, 8);

float distanceSquared = Vec3.DistanceSq(pointA, pointB);
if (distanceSquared < 4*4) { 
	Log.Info("Distance is less than 4");
}
```
### Configuring Quality Occlusion

If you expect the user's environment to change a lot, or you
anticipate the user's environment may not be well scanned already,
then you may wish to boost the frequency of world data updates. By
default, StereoKit is quite conservative about scanning to reduce
computation, but this can be configured using the World.RefreshX
properties as seen here.

```csharp
// If occlusion is not available, the rest of the code will have no
// effect.
if (!SK.System.worldOcclusionPresent)
	Log.Info("Occlusion not available!");

// Configure SK to update the world data as fast as possible, this
// allows occlusion to accomodate better for moving objects.
World.OcclusionEnabled = true;
World.RefreshType     = WorldRefresh.Timer; // Refresh on a timer
World.RefreshInterval = 0; // Refresh every 0 seconds
World.RefreshRadius   = 6; // Get everything in a 6m radius
```
```csharp
// Here's some quick and dirty lines for the play boundary rectangle!
if (World.HasBounds)
{
	Vec2   s    = World.BoundsSize/2;
	Matrix pose = World.BoundsPose.ToMatrix();
	Vec3   tl   = pose.Transform( new Vec3( s.x, 0,  s.y) );
	Vec3   br   = pose.Transform( new Vec3(-s.x, 0, -s.y) );
	Vec3   tr   = pose.Transform( new Vec3(-s.x, 0,  s.y) );
	Vec3   bl   = pose.Transform( new Vec3( s.x, 0, -s.y) );

	Lines.Add(tl, tr, Color.White, 1.5f*U.cm);
	Lines.Add(bl, br, Color.White, 1.5f*U.cm);
	Lines.Add(tl, bl, Color.White, 1.5f*U.cm);
	Lines.Add(tr, br, Color.White, 1.5f*U.cm);
}
```
### Basic World Occlusion

A simple example of turning on the occlusion mesh and overriding the
default material so it's visible. For normal usage where you just
want to let the real world occlude geometry, the only important
element is to just set `World.OcclusionEnabled = true;`.
```csharp
Material occlusionMatPrev;

public void Start()
{
	if (!SK.System.worldOcclusionPresent)
		Log.Info("Occlusion not available!");

	// If not available, this will have no effect
	World.OcclusionEnabled = true;

	// Override the default occluding material
	occlusionMatPrev = World.OcclusionMaterial;
	World.OcclusionMaterial = Material.Default;
}

public void Stop()
{
	// Restore the previous occlusion material
	World.OcclusionMaterial = occlusionMatPrev;

	// Stop occlusion
	World.OcclusionEnabled = false;
}
```
### Basic World Raycasting

World.RaycastEnabled must be true before calling World.Raycast, or
you won't ever intersect with any world geometry.
```csharp
public void Start()
{
	if (!SK.System.worldRaycastPresent)
		Log.Info("World raycasting not available!");

	// This must be enabled before calling World.Raycast
	World.RaycastEnabled = true;
}

public void Stop() => World.RaycastEnabled = false;

public void Step()
{
	// Raycast out the index finger of each hand, and draw a red sphere
	// at the intersection point.
	for (int i = 0; i < 2; i++)
	{
		Hand hand = Input.Hand(i);
		if (!hand.IsTracked) continue;

		Ray fingerRay = hand[FingerId.Index, JointId.Tip].Pose.Ray;
		if (World.Raycast(fingerRay, out Ray at))
			Mesh.Sphere.Draw(Material.Default, Matrix.TS(at.position, 0.03f), new Color(1, 0, 0));
	}
}
```
# Debugging your App
### Set up for debugging
Since StereoKit's core is composed of native code, there are a few extra steps you can take to get better stack traces and debug information! The first is to make sure Visual Studio is set up to debug with native code. This varies across .Net versions, but generally the option can be found at Project->Properties->Debug->(Native code debugging).

You may also wish to disable "Just My Code" if you're trying to actually inspect how StereoKit's code is behaving. This can be found under Tools->Options->Debugging->General->Enable Just My Code, and uncheck it to make sure it's disabled.

StereoKit is set up with Source Link as of v0.3.5, which allows you to inspect StereoKit's code directly from the relevant commit of the main repository on GitHub. Note that distributed binaries are in release format, and may not 'step through' as nicely as a normal debug binary would.

### Check the Logs!
StereoKit outputs a lot of useful information in the logs, and there's a chance your issue may be logged there! When submitting an issue on the GitHub repository, including a copy of your logs can really help maintainers to understand what is or isn't happening.

All platforms will output the log through the standard debug output window, but you can also tap into the debug logs via [`Log.Subscribe`]({{site.url}}/Pages/Reference/Log/Subscribe.html). Check the docs there for an easy Mixed Reality log window you can add to your project.

### Ask for Help
We love to hear what problems you're running into! StereoKit is completely open source and has no analytics or surveillance tools embedded in it at all. If you have an issue, we won't know about it unless _you_ tell us, or we spot it ourselves!

The best place to ask for help will always be the [GitHub Issues](https://github.com/StereoKit/StereoKit/issues), or [GitHub Discussions](https://github.com/StereoKit/StereoKit/discussions) pages. Be sure to provide logs, platform information, and as many other details as may be relevant!

## Common Issues
Here's a short list of some common issues we've seen people ask about!

### XR_ERROR_FORM_FACTOR_UNAVAILABLE in the logs
This is a common and expected message that basically means that OpenXR can't find any headset attached to the system. Your headset is most likely unplugged, but may also indicate some other issue with your OpenXR runtime setup.

By default, StereoKit will fall back to the flatscreen simulator when this error message is encountered. This behavior can be configured in your `SKSettings`.

### StereoKit isn't loading my asset!
This may manifest as Null Reference Exceptions surrounding your Model/Tex/asset. The first thing to do is check the StereoKit logs, and look for messages with your asset's filename. There will likely be some message with a decent hint available.

If StereoKit cannot find your file, make sure the path is correct, and verify your asset is correctly being copied into Visual Studio's output folder. The default templates will automatically copy content in the project's Assets folder into the final output folder. If your asset is not in the Assets folder, or if you have assembled your own project without using the templates, then you may need to do additional work to ensure the copy happens.

### System.DLLNotFoundException for StereoKitC
A StereoKit function has been called before the native StereoKit DLL was loaded. Make sure your code is happening _after_ your call to `SK.Initialize`! Watch out for code being called from implied constructors, especially on static classes.

For some rare cases where you need access to a StereoKit function before initialization, you may be able to call `SK.PreLoadLibrary`. This only works for functions that interact with code that does not require initialization, like math. It may also disguise code that's incorrectly being called before SK.Initialize.
# Drawing content with StereoKit

Generally, the first thing you want to do is get content on-screen! Or
in-visor? However it's said, in this guide we're going to explore the
various ways to display some holograms!

At its core, drawing things in 3D is done through a combination of
[`Mesh`]({{site.url}}/Pages/Reference/Mesh.html)es and
[`Material`]({{site.url}}/Pages/Reference/Material.html)s. A Mesh
is a collection of triangles in 3D space that describe where the
surface of that 3D object is. And a Material is then a collection
of parameters, [`Tex`]({{site.url}}/Pages/Reference/Tex.html)tures
(2d images), and Shader code that are combined to describe the
visual properties of the Mesh's surface!

![Meshes are made from triangles!]({{site.screen_url}}/Drawing_MeshLooksLike.jpg)
_Meshes are made from triangles!_

And in addition to that, you'll need to know a little bit about
matrices, which are a math construct used to describe the location,
orientation and scale of geometry within the 3D space! A [`Matrix`]({{site.url}}/Pages/Reference/Matrix.html)
isn't difficult the way we're using it, so don't worry if math
isn't your thing.

And then StereoKit also has a [`Model`]({{site.url}}/Pages/Reference/Model.html),
which is a high level combination of Meshes, Material, Matrices,
and a few more things! Most of the time, you'll probably be drawing
Models loaded from file, but it's important to have options.

Then lastly, StereoKit has easy systems for drawing [`Line`]({{site.url}}/Pages/Reference/Lines.html)s,
[`Text`]({{site.url}}/Pages/Reference/Text.html), [`Sprite`]({{site.url}}/Pages/Reference/Sprite.html)s
and various other things! These are still based on Meshes and
Materials under the hood, but have some complex features that can
make them difficult to build from scratch.

## Meshes and Materials

To simplify things here, we're going to use the built-in assets,
[`Mesh.Sphere`]({{site.url}}/Pages/Reference/Mesh/Sphere.html)
and [`Material.Default`]({{site.url}}/Pages/Reference/Material/Default.html).
Mesh.Sphere is a built-in mesh generated using math when StereoKit
starts up, and Material.Default is a high performance simple
Material that serves as StereoKit's default Material. (For more
built-in assets, see the [`Default`]({{site.url}}/Pages/Reference/Default.html)s)

```csharp
Mesh.Sphere.Draw(Material.Default, Matrix.Identity);
```

![Default sphere and material]({{site.screen_url}}/Drawing_Defaults.jpg)
_Drawing the default sphere Mesh with the default Material._

[`Matrix.Identity`]({{site.url}}/Pages/Reference/Matrix/Identity.html)
can be though of as a 'No transform' Matrix, so this is drawing the
sphere at the origin of the 3D space.

That's pretty straightforward! StereoKit will get this Mesh/Material
pair onto the screen this frame. Remember that StereoKit is
generally an immediate mode API, so this won't show up for more
than the current frame. If you want it to draw every frame, you'll
have to call Draw every frame!

So how do you get a Mesh to begin with? In most cases you'll just
be working with Models, but you can get a Mesh directly from a few
places:
 - [`Mesh.Sphere`]({{site.url}}/Pages/Reference/Mesh/Sphere.html), [`Mesh.Cube`]({{site.url}}/Pages/Reference/Mesh/Cube.html), and [`Mesh.Quad`]({{site.url}}/Pages/Reference/Mesh/Quad.html) are built-in mesh assets that are handy to have around.
 - [`Mesh`]({{site.url}}/Pages/Reference/Mesh.html) has a number of static methods for generating procedural shapes, such as [`Mesh.GenerateRoundedCube`]({{site.url}}/Pages/Reference/Mesh/GenerateRoundedCube.html) or [`Mesh.GeneratePlane`]({{site.url}}/Pages/Reference/Mesh/GeneratePlane.html).
 - A Mesh can be extracted from one of a [Model's nodes]({{site.url}}/Pages/Reference/ModelNode/Mesh.html).
 - You can create a Mesh from a list of vertices and indices. This is more advanced, but [check the sample here]({{site.url}}/Pages/Reference/Mesh/SetVerts.html).

And where do you get a Material? Well,
 - See built-in Materials like [`Material.PBR`]({{site.url}}/Pages/Reference/Default/MaterialPBR.html) for high-quality surface or [`Material.Unlit`]({{site.url}}/Pages/Reference/Default/MaterialUnlit.html) for fast/stylistic surfaces.
 - A Material [constructor]({{site.url}}/Pages/Reference/Material/Material.html) can be called with a Shader. Check out [the Material guide]({{site.url}}/Pages/Guides/Working-with-Materials.html) for in-depth usage (Materials and Shaders are a lot of fun!).
 - You can call [`Material.Copy`]({{site.url}}/Pages/Reference/Material/Copy.html) to create a duplicate of an existing Material.

## Matrix basics

If you like math, this explanation is not really for you! But if
you like results, this will get you going where you need to go. The
important thing to know about a [`Matrix`]({{site.url}}/Pages/Reference/Matrix.html),
is that it's a good way to represent an object's transform (Translation,
Rotation, and Scale).

StereoKit provides a number of Matrix creation methods that allow
you to easily create Translation/Rotation/Scale matrices.
```csharp
// The identity matrix is the matrix equivalent of '1'. You can also
// think of it as a 'no-transform' matrix.
Matrix transform = Matrix.Identity;

// Translates points 1 meter up the Y axis
transform = Matrix.T(0, 1, 0);

// Scales a point by (2,2,2), rotates it 180 on the X axis, and
// then translates it up 1 meter up the Y axis.
transform = Matrix.TRS(
	new Vec3(0,1,0),
	Quat.FromAngles(180, 0, 0),
	new Vec3(2,2,2));

// To draw a cube at (0,-10,0) that's rotated 45 degrees around its Y
// axis:
Mesh.Cube.Draw(Material.Default, Matrix.TR(0,-10,0, Quat.FromAngles(0,45,0)));
```

The TRS methods have a lot of permutations that can help make your
matrix creation code a bit shorter. Like, if you don't need to add
scale to your TRS matrix, there's the TR variant! No rotation? Try
TS! Etc. etc.

Now. Even _more_ interesting, is that many Matrices can be combined
into a single Matrix representing multiple transforms! This is done
via multiplication, and an important note here is that matrix
multiplication is not commutative, that is: `A*B != B*A`, so the
order in which you combine your matrices is important.

This can let you do things like, rotate around a pivot point, or
build a hierarchy of transforms! A parent/child position hierarchy
can be described pretty easily this way:
```csharp
Matrix parentTransform = Matrix.TR(10, 0, 0, Quat.FromAngles(0, 45, 0));
Matrix childTransform  = Matrix.TS( 1, 1, 0, 0.2f);

Mesh.Cube.Draw(Material.Default, parentTransform);
Mesh.Cube.Draw(Material.Default, childTransform * parentTransform);
```

![Combining matrices]({{site.screen_url}}/Drawing_MatrixCombine.jpg)
_The smaller `childTransform` is relative to `parentTransform` via multiplication._

## Models

The easiest way to draw complex content is through a Model! A Model
is basically a small scene of Mesh/Material pairs at positions with
hierarchical relationships to each other. If you're creating art in
a 3D modeling tool such as Blender, then this is basically a full
representation of the scene you've created there.

Since a model already has all its information within it, all you
need to do is provide it with a location!
```csharp
model.Draw(Matrix.T(10, 10, 0));
```
![Drawing a model]({{site.screen_url}}/Drawing_Model.jpg)
_StereoKit's main format is the .gltf file._

So... that was also pretty simple! The only real trick with Models
is getting one in the first place, but even that's not too hard.
There's a lot you can do with a Model beyond just drawing it, so
for more details on that, check out [the Model guide](https://github.com/StereoKit/StereoKit/blob/master/Examples/StereoKitTest/Demos/DemoNodes.cs) (coming soon)!

But here's the quick list of where you can get a Model to begin
with:
 - [`Model.FromFile`]({{site.url}}/Pages/Reference/Model/FromFile.html) is the easiest, most common way to get a Model!
 - [`Model.FromMesh`]({{site.url}}/Pages/Reference/Model/FromMesh.html) will let you create a very simple Model with a single function call.
 - The [Model constructor]({{site.url}}/Pages/Reference/Model/Model.html) lets you create an empty Model, which you can then fill with ModelNodes via [`Model.AddNode`]({{site.url}}/Pages/Reference/Model/AddNode.html)
 - You can call [`Model.Copy`]({{site.url}}/Pages/Reference/Model/Copy.html) to create a duplicate of an existing Model.

## Lines

Being able to easily draw a line is incredibly useful for
debugging, and generally quite practical for many other purposes as
well! StereoKit has the [`Lines`]({{site.url}}/Pages/Reference/Lines.html)
class to assist with this, and is pretty straightforward to use.
There's a few variations, but at it's simplest, it's a few points,
a color, and a thickness.
```csharp
Lines.Add(
	new Vec3(2, 2, 0),
	new Vec3(3, 2.5f, 0),
	Color.Black, 1*U.cm);
```
![Drawing a line]({{site.screen_url}}/Drawing_Lines.jpg)
_You can also draw Rays, Poses, and multicolored lists of lines!_

## Text

Text is drawn with a collection of rectangular quads, each mapped
to a character glyph on a texture. StereoKit supports rendering any
Unicode glyphs you throw at it, as long as the active Font has
that glyph defined in it! This means you can work with all sorts of
different languages right away, without any baking or preparation.

To draw text with the default Font, you can do this!
```csharp
Text.Add("こんにちは", Matrix.T(-10, 10,0));
```

![Drawing text]({{site.screen_url}}/Drawing_Text.jpg)
_'Hello' in Japanese, I'm pretty sure._

You can create additional font styles and fonts to use with text
drawing, and there are a number of overloads for [`Text.Add`]({{site.url}}/Pages/Reference/Text/Add.html)
that allow you to change the layout or constrain to a particular
area. Check the docs for the method for more information about that!

## Cool!

So that's the highlights! There's plenty more to draw and more
tricks to be learned, but this is a great start! There's treasures
in the documentation, so hunt around in there for more samples. You
may also be interested in the [Materials guide]({{site.url}}/Pages/Guides/Working-with-Materials.html)
for advanced rendering code, or the [Model guide](https://github.com/StereoKit/StereoKit/blob/master/Examples/StereoKitTest/Demos/DemoNodes.cs)
(coming soon), for managing your visible content!

If you'd like to see all the code for this document,
[check it out here!](https://github.com/StereoKit/StereoKit/blob/master/Examples/StereoKitTest/Guides/GuideDrawing.cs)
# Getting Started with StereoKit

Here's a quick list of what you'll need to start developing with StereoKit:

- **[Visual Studio 2019 or 2022](https://visualstudio.microsoft.com/vs/) - Use these workloads:**
  - .NET Desktop development
  - Universal Windows Platform development (for HoloLens)
  - Mobile development with .Net (for Quest)
- **[StereoKit's Visual Studio Template](https://marketplace.visualstudio.com/items?itemName=NickKlingensmith.StereoKitTemplates)**
  - Experienced users might directly use the [NuGet package](https://www.nuget.org/packages/StereoKit).
- **Any OpenXR runtime**
  - A flatscreen fallback is available for development.
- **Enable Developer Mode (for UWP/HoloLens)**
  - Windows Settings->Update and Security->For Developers->Developer Mode

This short video goes through the pre-requisites for building StereoKit's
hello world! You can find a [UWP/HoloLens specific version here](https://www.youtube.com/watch?v=U_7VNIcPQaM)
as well.
<iframe width="560" height="315" src="https://www.youtube-nocookie.com/embed/lOYs8seoRpc" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>

For Mac developers: while StereoKit's _simulator_ does not run on Mac OS,
you can still deploy to standalone Android headsets such as Quest!
[See here](https://www.youtube.com/watch?v=UMwTLecVATU) for a quick video
by community member Raphael about how to do this with the experimental
cross platform template.

## The Templates

![Create New Project]({{site.url}}/img/screenshots/VSNewProject.png)

- **StereoKit .Net Core**
  - .Net Core is for desktop XR on Windows and Linux. It is simple, compiles quickly, and is the best option for most developers.
- **StereoKit UWP**
  - UWP is for HoloLens 2, and can run on Windows desktop. UWP can be slower to compile, and is no longer receiving updates from the .Net team.
- _[Cross Platform/Universal Template (in development)](https://github.com/StereoKit/SKTemplate-Universal)_
  - This is an early version still in project format. It works with .Net Core, UWP, and Xamarin(Android/Quest) all at once via a DLL shared between multiple platform specific projects.
- **[Native C/C++ Template](https://github.com/StereoKit/SKTemplate-CMake)**
  - StereoKit does provide a C API, but experienced developers should only choose this if the benefits outweigh the lack of C API documentation.

For an overview of the initial code in the .Net Core and UWP templates,
check out this video!
<iframe width="560" height="315" src="https://www.youtube-nocookie.com/embed/apcWlHNJ5kM" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>

## Minimum "Hello Cube" Application

The template does provide some code to help provide new developers a base
to work from, but what parts of the code are really necessary? We can boil
"Hello Cube" down to something far simpler if we want to! This is the
simplest possible StereoKit application:

```csharp
using StereoKit;

class Program
{
	static void Main(string[] args)
	{
		SK.Initialize(new SKSettings{ appName = "Project" });

		SK.Run(() =>
		{
			Mesh.Cube.Draw(Material.Default, Matrix.S(0.1f));
		});
	}
}
```

## Next Steps

Awesome! That's pretty great, but what next? [Why don't we build some UI]({{site.url}}/Pages/Guides/User-Interface.html)?
Alternatively, you can check out the [StereoKit Ink](https://github.com/StereoKit/StereoKit-PaintTutorial)
repository, which contains an XR ink-painting application written in about
220 lines of code! It's well commented, and is a good example to pick
through.

For additional learning resources, you can check out the [Learning Resources]({{site.url}}/Pages/Guides/Learning-Resources.html)
page for a couple of repositories and links that may help you out. In
particular, the GitHub repository does contain a [number of small demo scenes](https://github.com/StereoKit/StereoKit/tree/master/Examples/StereoKitTest/Demos)
that are excellent reference for a number of different StereoKit features!

And don't forget to peek in the docs here! Most pages contain sample code
that illustrates how a particular function or property is used
in-context. The ultimate goal is to have a sample for 100% of the docs,
so if you're looking for one and it isn't there, use the 'Create an Issue'
link at the bottom of the web page to get it prioritized!
# Learning Resources

Outside of the resources here on the StereoKit site, there's a number of
other places you can go for learning information! Here's a collection of
external learning and sample resources to get you off the ground a little
faster! If you have your own resources that you'd like to see linked
here, just let us know!

## Sample Projects

### [StereoKit Ink](https://github.com/StereoKit/StereoKit-PaintTutorial)

![StereoKit Ink]({{site.screen_url}}/StereoKitInk.jpg)

A well documented repository that illustrates creating a complete but
simplified inking application. It includes functionality like custom and
standard UI, line rendering, file save/load, and hand menus.

### [Bing Maps API and Terrain Sample](https://github.com/StereoKit/StereoKit-BingMaps)

![Bing Maps API and Terrain Sample]({{site.screen_url}}/SKMapsTutorial.jpg)

A well documented repository showing how to load and display satellite
imagery and elevation information from the Bing Maps API. It includes
creating a terrain system using StereoKit's shader API, loading color and
height data from an external API, and building a pedestal interface to
interact with the content.

### [Release Notes Demo for v0.3.1](https://github.com/StereoKit/StereoKitReleaseNotes/tree/main/v0.3.1)

This is an interactive release notes demo project that showcases the
features released in StereoKit v0.3.1! Not every release has a demo like
this, but it can be pretty enlightening to browse through a code-base
such as this one for reference.

### [GitHub Demos folder](https://github.com/StereoKit/StereoKit/tree/master/Examples/StereoKitTest/Demos)

These are the demos I build to test StereoKit features and APIs! They
are occasionally documented, but frequently short and concise. They
can be great to check out for a focused example of certain parts of
the API!

### [GitHub Discussions/Issues](https://github.com/StereoKit/StereoKit/discussions)

The best place to ask a question! It's asynchronous, and a great place
for long-form answers that can also benefit others. The Discussions tab
is best for questions, feedback, and more nebulous stuff, and the Issues
tab is best if you think something might be misbehaving or missing!

### [The StereoKit Discord Channel](https://discord.gg/jtZpfS7nyK)

In a rush with a question, got a project to share, or just want to hang
out and chat? Or maybe you're looking for some feedback on a potential
contribution? Whatever the case, come and say hi on the Discord! This is
the core hang-out spot for the team and community :)

# Building UI in StereoKit

StereoKit uses an immediate mode UI system. Basically, you define the UI
every single frame you want to see it! Sounds a little odd at first, but
it does have some pretty tremendous advantages. Since very little state
is actually stored, you can add, remove, and change your UI elements with
trivial and standard code structures! You'll find that you often have
much less UI code, with far fewer places for things to go wrong.

The goal for this UI API is to get you up and running as fast as possible
with a working UI! This does mean trading some design flexibility for API
simplicity, but we also strive to retain configurability for those that
need a little extra.

## Making a Window

![Simple UI]({{site.url}}/img/screenshots/GuideUserInterface.jpg)

Since StereoKit doesn't store state, you'll have to keep track of
your data yourself! But that's actually a pretty good thing, since
you'll probably do that one way or another anyhow. Here we've got a
Pose for the window, off to the left and facing to the right, as well
as a boolean for a toggle, and a float that we'll use as a slider!
We'll add this code to our initialization section.
```csharp
Pose  windowPose = new Pose(-.4f, 0, 0, Quat.LookDir(1,0,1));

bool  showHeader = true;
float slider     = 0.5f;

Sprite powerSprite = Sprite.FromFile("power.png", SpriteType.Single);
```
Then we'll move over to the application step where we'll do the
rest of the UI code!

We'll start with a window titled "Window" that's 20cm wide, and
auto-resizes on the y-axis. The U class is pretty helpful here,
as it allows us to reason more visually about the units we're
using! StereoKit uses meters as its base unit, which look a
little awkward as raw floats, especially in the millimeter range.

We'll also use a toggle to turn the window's header on and off!
The value from that toggle is passed in here via the showHeader
field.

```csharp
UI.WindowBegin("Window", ref windowPose, new Vec2(20, 0) * U.cm, showHeader?UIWin.Normal:UIWin.Body);
```

When you begin a window, all visual elements are now relative to
that window! UI takes advantage of the Hierarchy class and pushes
the window's pose onto the Hierarchy stack. Ending the window
will pop the pose off the hierarchy stack, and return things to
normal!

Here's that toggle button! You'll also notice our use of 'ref'
values in a lot of the UI code. UI functions typically follow the
pattern of returning true/false to indicate they've been
interacted with during the frame, so you can nicely wrap them in
'if' statements to react to change!

Then with the 'ref' parameter, we let you pass in the current
state of the UI element. The UI element will update that value
for you based on user interaction, but you can also change it
yourself whenever you want to!

```csharp
UI.Toggle("Show Header", ref showHeader);
```

Here's an example slider! We start off with a label element, and
tell the UI to keep the next item on the same line. The slider
clamps to the range [0,1], and will step at intervals of 0.2. If
you want it to slide continuously, you can just set the `step`
value to 0!

```csharp
UI.Label("Slide");
UI.SameLine();
UI.HSlider("slider", ref slider, 0, 1, 0.2f, 72 * U.mm);
```

Here's how you use a simple button! Just check it with an 'if'.
Any UI method will return true on the frame when their value or
state has changed.

```csharp
if (UI.ButtonImg("Exit", powerSprite))
	SK.Quit();
```

And for every begin, there must also be an end! StereoKit will
log errors when this occurs, so keep your eyes peeled for that!

```csharp
UI.WindowEnd();
```

## Custom Windows

![Simple UI]({{site.url}}/img/screenshots/GuideUserInterfaceCustom.jpg)

Mixed Reality also provides us with the opportunity to turn
objects into interfaces! Instead of using the old 'window'
paradigm, we can create 3D models and apply UI elements to their
surface! StereoKit uses 'handles' to accomplish this, a grabbable
area that behaves much like a window, but with a few more options
for customizing layout and size.

We'll load up a clipboard, so we can attach an interface to that!

```csharp
Model clipboard = Model.FromFile("Clipboard.glb");
```

And, similar to the window previously, here's how you would turn
it into a grabbable interface! This behaves the same, except
we're defining where the grabbable region is specifically, and
then drawing our own model instead of a plain bar. You'll also
notice we're drawing using an identity matrix. This takes
advantage of how HandleBegin pushes the handle's pose onto the
Hierarchy transform stack!

```csharp
UI.HandleBegin("Clip", ref clipboardPose, clipboard.Bounds);
clipboard.Draw(Matrix.Identity);
```

Once we've done that, we also need to define the layout area of
the model, where UI elements will go. This is different for each
model, so you'll need to plan this around the size of your
object!

```csharp
UI.LayoutArea(new Vec3(12, 15, 0) * U.cm, new Vec2(24, 30) * U.cm);
```

Then after that? We can just add UI elements like normal!

```csharp
UI.Image(logoSprite, new Vec2(22,0) * U.cm);

UI.Toggle("Toggle", ref clipToggle);
UI.HSlider("Slide", ref clipSlider, 0, 1, 0, 22 * U.cm);
```

And while we're at it, here's a quick example of doing a radio
button group! Not much 'radio' actually happening, but it's still
pretty simple. Pair it with an enum, or an integer, and have fun!

```csharp
if (UI.Radio("Radio1", clipOption == 1)) clipOption = 1;
UI.SameLine();
if (UI.Radio("Radio2", clipOption == 2)) clipOption = 2;
UI.SameLine();
if (UI.Radio("Radio3", clipOption == 3)) clipOption = 3;
```

As with windows, Handles need an End call.

```csharp
UI.HandleEnd();
```

## An Important Note About IDs

StereoKit does store a small amount of information about the UI's
state behind the scenes, like which elements are active and for
how long. This internal data is attached to the UI elements via
a combination of their own ids, and the parent Window/Handle's
id!

This means you should be careful to NOT re-use ids within a
Window/Handle, otherwise you may find ghost interactions with
elements that share the same ids. If you need to have elements
with the same id, or if perhaps you don't know in advance that
all your elements will certainly be unique, UI.PushId and
UI.PopId can be used to mitigate the issue by using the same
hierarchy id mixing that the Windows use to prevent collisions
with the same ids in other Windows/Handles.

Here's the same set of radio options, but all of them have the
same name/id!

```csharp
UI.PushId(1);
if (UI.Radio("Radio", clipOption == 1)) clipOption = 1;
UI.PopId();

UI.SameLine();
UI.PushId(2);
if (UI.Radio("Radio", clipOption == 2)) clipOption = 2;
UI.PopId();

UI.SameLine();
UI.PushId(3);
if (UI.Radio("Radio", clipOption == 3)) clipOption = 3;
UI.PopId();
```
## What's Next?

And there you go! That's how UI works in StereoKit, pretty
simple, huh? For further reference, and more UI methods, check
out the [UI class documentation]({{site.url}}/Pages/Reference/UI.html).

If you'd like to see the complete code for this sample,
[check it out on Github](https://github.com/StereoKit/StereoKit/blob/master/Examples/StereoKitTest/Demos/DemoUI.cs)!
# Using Hands

StereoKit uses a hands first approach to user input! Even when hand-sensors
aren't available, hand data is simulated instead using existing devices!
For example, Windows Mixed Reality controllers will blend between pre-recorded
hand poses based on button presses, as will mice. This way, fully articulated
hand data is always present for you to work with!

## Accessing Joints

![Hand with joints]({{site.url}}/img/screenshots/HandAxes.jpg)

Since hands are so central to interaction, accessing hand information needs
to be really easy to get! So here's how you might find the fingertip of the right
hand! If you ignore IsTracked, this'll give you the last known position for that
finger joint.
```csharp
Hand hand = Input.Hand(Handed.Right);
if (hand.IsTracked)
{ 
	Vec3 fingertip = hand[FingerId.Index, JointId.Tip].position;
}
```
Pretty straightforward! And if you prefer calling a function instead of using the
[] operator, that's cool too! You can call `hand.Get(FingerId.Index, JointId.Tip)`
instead!

If that's too granular for you, there's easy ways to check for pinching and
gripping! Pinched will tell you if a pinch is currently happening, JustPinched
will tell you if it just started being pinched this frame, and JustUnpinched will
tell you if the pinch just stopped this frame!
```csharp
if (hand.IsPinched) { }
if (hand.IsJustPinched) { }
if (hand.IsJustUnpinched) { }

if (hand.IsGripped) { }
if (hand.IsJustGripped) { }
if (hand.IsJustUngripped) { }
```
These are all convenience functions wrapping the `hand.pinchState` bit-flag, so you
can also use that directly if you want to do some bit-flag wizardry!
## Hand Menu

Lets imagine you want to make a hand menu, you might need to know
if the user is looking at the palm of their hand! Here's a quick
example of using the palm's pose and the dot product to determine
this.
```csharp
static bool HandFacingHead(Handed handed)
{
	Hand hand = Input.Hand(handed);
	if (!hand.IsTracked)
		return false;

	Vec3 palmDirection   = (hand.palm.Forward).Normalized;
	Vec3 directionToHead = (Input.Head.position - hand.palm.position).Normalized;

	return Vec3.Dot(palmDirection, directionToHead) > 0.5f;
}
```
Once you have that information, it's simply a matter of placing a
window off to the side of the hand! The palm pose Right direction
points to different sides of each hand, so a different X offset
is required for each hand.
```csharp
public static void DrawHandMenu(Handed handed)
{
	if (!HandFacingHead(handed))
		return;

	// Decide the size and offset of the menu
	Vec2  size   = new Vec2(4, 16);
	float offset = handed == Handed.Left ? -2-size.x : 2+size.x;

	// Position the menu relative to the side of the hand
	Hand hand   = Input.Hand(handed);
	Vec3 at     = hand[FingerId.Little, JointId.KnuckleMajor].position;
	Vec3 down   = hand[FingerId.Little, JointId.Root        ].position;
	Vec3 across = hand[FingerId.Index,  JointId.KnuckleMajor].position;

	Pose menuPose = new Pose(
		at,
		Quat.LookAt(at, across, at-down) * Quat.FromAngles(0, handed == Handed.Left ? 90 : -90, 0));
	menuPose.position += menuPose.Right * offset * U.cm;
	menuPose.position += menuPose.Up * (size.y/2) * U.cm;

	// And make a menu!
	UI.WindowBegin("HandMenu", ref menuPose, size * U.cm, UIWin.Empty);
	UI.Button("Test");
	UI.Button("That");
	UI.Button("Hand");
	UI.WindowEnd();
}
```
## Pointers

And lastly, StereoKit also has a pointer system! This applies to
more than just hands. Head, mouse, and other devices will also
create pointers into the scene. You can filter pointers based on
source family and device capabilities, so this is a great way to
abstract a few more input sources nicely!
```csharp
public static void DrawPointers()
{
	int hands = Input.PointerCount(InputSource.Hand);
	for (int i = 0; i < hands; i++)
	{
		Pointer pointer = Input.Pointer(i, InputSource.Hand);
		Lines.Add    (pointer.ray, 0.5f, Color.White, Units.mm2m);
		Lines.AddAxis(pointer.Pose);
	}
}
```
The code in context for this document can be found [on Github here](https://github.com/StereoKit/StereoKit/blob/master/Examples/StereoKitTest/DemoHands.cs)!
# Using QR Codes

QR codes are a super fast and easy way to locate an object,
provide information from the environment, or `localize` two
devices to the same coordinate space! HoloLens 2 and WMR headsets
have a really convenient way to grab and use this data. They can use
the tracking cameras of the device, at the driver level to provide
QR codes from the environment, pretty much for free!

The only caveat is that tracking cameras are lower resolution, so
they need big QR codes, or to be very close to the codes. They also
only update around 2 times a second. But if that suits your needs?
Then you're in luck!

## Pre-Requisites

QR code support is not built directly in to StereoKit, but
it is quite trivial to implement! For this, we use the
[Microsoft MixedReality QR code library](https://docs.microsoft.com/en-us/windows/mixed-reality/qr-code-tracking)
through its NuGet package. This will require a UWP StereoKit
project, and the Webcam capability in the project's
.appxmanifest file.

So! That's the pre-reqs for this guide!

 - A StereoKit UWP project.
 - The [NuGet package](https://www.nuget.org/Packages/Microsoft.MixedReality.QR).
 - Enable the `Webcam` capability in the .appxmanifest file.

Then in your code, you'll be able to add this using
statement and get access to the `QRCodeWatcher`, the main
interface to the QR code functionality.
```csharp
using Microsoft.MixedReality.QR;
```
## Code

For code, we'll start with our own representation of
what a QR code means. Nothing fancy, we just want to
show the orientation and contents of each code! So, pose,
size, and data as text.

We'll also include a function to convert the WMR QR code into
our own. The only fancy stuff happening here is grabbing the
Pose! The `SpatialGraphNodeId` contains a pose, but it's in
UWPs coordinate space. `Pose.FromSpatialNode` is a bridge
function that will convert from UWP's coordinates into our own.
```csharp
struct QRData
{ 
	public Pose   pose;
	public float  size;
	public string text;
	public static QRData FromCode(QRCode qr)
	{
		QRData result = new QRData();
		// It's not unusual for this to fail to find a pose, especially on
		// the first frame it's been seen.
		World.FromSpatialNode(qr.SpatialGraphNodeId, out result.pose);
		result.size = qr.PhysicalSideLength;
		result.text = qr.Data == null ? "" : qr.Data;
		return result;
	}
}
```
Ok, cool! Now here's the data we'll be tracking for this demo,
the `QRCodeWatcher` is the object that'll provide us QR data,
`watcherStart` will let us filter out QR codes from other sesions,
and `poses` is our list of unique QR codes that we can iterate through
and draw.
```csharp
QRCodeWatcher watcher;
DateTime      watcherStart;
Dictionary<Guid, QRData> poses = new Dictionary<Guid, QRData>();
```
Initialization is just a matter of asking for permission, and then
hooking up to the `QRCodeWatcher`'s events. `QRCodeWatcher.RequestAccessAsync`
is an async call, so you could re-arrange this code to be non-blocking!

You'll also notice there's some code here for filtering out QR codes.
The default behavior for the QR code library is to provide all QR
codes that it knows about, and that includes ones that were found
before the session began. We don't need that, so we're ignoring those.
```csharp
public void Initialize()
{
	// Ask for permission to use the QR code tracking system
	var status = QRCodeWatcher.RequestAccessAsync().Result;
	if (status != QRCodeWatcherAccessStatus.Allowed)
		return;

	// Set up the watcher, and listen for QR code events.
	watcherStart = DateTime.Now;
	watcher      = new QRCodeWatcher();
	watcher.Added   += (o, qr) => {
		// QRCodeWatcher will provide QR codes from before session start,
		// so we often want to filter those out.
		if (qr.Code.LastDetectedTime > watcherStart) 
			poses.Add(qr.Code.Id, QRData.FromCode(qr.Code)); 
	};
	watcher.Updated += (o, qr) => poses[qr.Code.Id] = QRData.FromCode(qr.Code);
	watcher.Removed += (o, qr) => poses.Remove(qr.Code.Id);
	watcher.Start();
}

// For shutdown, we just need to stop the watcher
public void Shutdown() => watcher?.Stop();

```
Now all we need to do is show the QR codes! In this case,
we're just displaying an axis widget, and the contents of
the QR code as text.

With the text, all we're doing is squeezing the text into
the bounds of the QR code, and shifting it to be a little
forward, in front of the code!
```csharp
public void Update()
{
	foreach(QRData d in poses.Values)
	{ 
		Lines.AddAxis(d.pose, d.size);
		Text .Add(
			d.text, 
			d.pose.ToMatrix(),
			Vec2.One * d.size,
			TextFit.Squeeze,
			TextAlign.XLeft | TextAlign.YTop,
			TextAlign.Center,
			d.size, d.size);
	}
}
```
And that's all there is to it! You can find all this code
in context [here on Github](https://github.com/StereoKit/StereoKit/blob/master/Examples/StereoKitTest/Demos/DemoQRCode.cs).
# Using the Simulator

As a developer, you can't realistically spend all of your development in
a headset just yet. So, a decent grasp over StereoKit's fallback
flatscreen MR simulator is particularly helpful! This is basically a 2D
window that allows you to move around and interact, without requiring an
OpenXR runtime or headset.

## Simulator Controls

When you start the simulator, you'll find that your mouse controls the
right hand by default. This is a complete simulation of an articulated
hand, so you'll have access to all the joints the same way you would a
real tracked hand. The hand becomes tracked when the mouse enters the
window, and untracked when leaving the window. The pointer ray, which is
normally a shoulder->hand ray, will be along the mouse ray instead.

### Mouse Controls:
- Left Mouse - Hand animates to a Pinch gesture.
- Right Mouse - Hand animates to a Grip gesture.
- Left + Right - Hand animates to a closed fist.
- Scroll Wheel - Moves the hand toward or away from the user.
- Shift + Right - Mouse-look / rotate the head.
- Left Alt - [Eye tracking](({{site.url}}/Pages/Reference/Input/Eyes.html) will point along the ray indicated by the mouse.

To move around in space, you'll find controls that should be familiar to
those that play first-person games! Hold Left Shift to enable this.

### Keyboard Controls:
- Shift+W - Move forward.
- Shift+A - Move left.
- Shift+S - Move backwards.
- Shift+D - Move right.
- Shift+Q - Move down.
- Shift+E - Move up.

## Simulator API

There's a few bits of functionality that let you set up the simulator, or
some features that may assist you in debugging or testing! Here's a
couple you may want to know about:

### Simulator Enable/Disable

By default, StereoKit will fall back to the flatscreen simulator if
OpenXR fails to initialize for any reason (like, headset not plugged in,
or OpenXR not present). You can modify this behavior at initialization
time when defining your SKSettings for SK.Init.
```csharp
SKSettings settings = new SKSettings {
	appName                = "Flatscreen Simulator",
	assetsFolder           = "Assets",
	// This tells StereoKit to always start in a 2D flatscreen
	// window, instead of an immersive MR environment.
	displayPreference      = DisplayMode.Flatscreen,
	// Setting this to true will disable all built-in MR simulator
	// controls.
	disableFlatscreenMRSim = false,
	// Setting this to true will prevent StereoKit from creating the
	// fallback simulator when OpenXR fails to initialize. This is
	// important when shipping a final application to users.
	noFlatscreenFallback   = true,
};
```

### Overriding Hands

A number of functions are present that can make unit test and
complex input simulation possible. For a full example of this,
the [DebugToolWindow](https://github.com/StereoKit/StereoKit/blob/master/Examples/StereoKitTest/DebugToolWindow.cs)
in the Test project has a number of sample utilities for
recording and playing back input.

Overriding the hands is one important element that you may want
to do! [`Input.HandOverride`]({{site.url}}/Pages/Reference/Input/HandOverride.html)
will set the hand input to a very specific pose, and hold that
pose until you call `Input.HandOverride` again with a new pose,
or call [`Input.HandClearOverride`]({{site.url}}/Pages/Reference/Input/HandClearOverride.html)
to restore control back to the user.

![An overridden hand]({{site.screen_url}}/HandOverride.jpg)
_This screenshot is generated fresh every StereoKit release using Input.HandOverride, to ensure consistency!_
```csharp
// These 25 joints were printed using code from a session with a real
// hand.
HandJoint[] joints = new HandJoint[] { new HandJoint(new Vec3(0.132f, -0.221f, -0.168f), new Quat(-0.445f, -0.392f, 0.653f, -0.472f), 0.021f), new HandJoint(new Vec3(0.132f, -0.221f, -0.168f), new Quat(-0.445f, -0.392f, 0.653f, -0.472f), 0.021f), new HandJoint(new Vec3(0.141f, -0.181f, -0.181f), new Quat(-0.342f, -0.449f, 0.618f, -0.548f), 0.014f), new HandJoint(new Vec3(0.139f, -0.151f, -0.193f), new Quat(-0.409f, -0.437f, 0.626f, -0.499f), 0.010f), new HandJoint(new Vec3(0.141f, -0.133f, -0.198f), new Quat(-0.409f, -0.437f, 0.626f, -0.499f), 0.010f), new HandJoint(new Vec3(0.124f, -0.229f, -0.172f), new Quat(0.135f, -0.428f, 0.885f, -0.125f), 0.024f), new HandJoint(new Vec3(0.103f, -0.184f, -0.209f), new Quat(0.176f, -0.530f, 0.774f, -0.299f), 0.013f), new HandJoint(new Vec3(0.078f, -0.153f, -0.225f), new Quat(0.173f, -0.645f, 0.658f, -0.349f), 0.010f), new HandJoint(new Vec3(0.061f, -0.135f, -0.228f), new Quat(-0.277f, 0.674f, -0.623f, 0.283f), 0.010f), new HandJoint(new Vec3(0.050f, -0.125f, -0.227f), new Quat(-0.277f, 0.674f, -0.623f, 0.283f), 0.010f), new HandJoint(new Vec3(0.119f, -0.235f, -0.172f), new Quat(0.147f, -0.399f, 0.847f, -0.318f), 0.024f), new HandJoint(new Vec3(0.088f, -0.200f, -0.211f), new Quat(0.282f, -0.603f, 0.697f, -0.268f), 0.012f), new HandJoint(new Vec3(0.056f, -0.169f, -0.216f), new Quat(-0.370f, 0.871f, -0.308f, 0.099f), 0.010f), new HandJoint(new Vec3(0.045f, -0.156f, -0.195f), new Quat(-0.463f, 0.884f, -0.022f, -0.066f), 0.010f), new HandJoint(new Vec3(0.047f, -0.155f, -0.178f), new Quat(-0.463f, 0.884f, -0.022f, -0.066f), 0.010f), new HandJoint(new Vec3(0.111f, -0.244f, -0.173f), new Quat(0.182f, -0.436f, 0.778f, -0.414f), 0.022f), new HandJoint(new Vec3(0.074f, -0.213f, -0.205f), new Quat(-0.353f, 0.622f, -0.656f, 0.244f), 0.011f), new HandJoint(new Vec3(0.046f, -0.189f, -0.204f), new Quat(-0.436f, 0.891f, -0.073f, -0.108f), 0.010f), new HandJoint(new Vec3(0.048f, -0.184f, -0.182f), new Quat(-0.451f, 0.811f, 0.264f, -0.263f), 0.010f), new HandJoint(new Vec3(0.061f, -0.188f, -0.168f), new Quat(-0.451f, 0.811f, 0.264f, -0.263f), 0.010f), new HandJoint(new Vec3(0.105f, -0.250f, -0.170f), new Quat(0.219f, -0.470f, 0.678f, -0.521f), 0.020f), new HandJoint(new Vec3(0.062f, -0.228f, -0.196f), new Quat(-0.444f, 0.610f, -0.623f, 0.206f), 0.010f), new HandJoint(new Vec3(0.044f, -0.215f, -0.192f), new Quat(-0.501f, 0.841f, -0.094f, -0.183f), 0.010f), new HandJoint(new Vec3(0.048f, -0.209f, -0.176f), new Quat(-0.521f, 0.682f, 0.251f, -0.448f), 0.010f), new HandJoint(new Vec3(0.061f, -0.207f, -0.168f), new Quat(-0.521f, 0.682f, 0.251f, -0.448f), 0.010f), new HandJoint(new Vec3(0.098f, -0.222f, -0.191f), new Quat(0.308f, -0.906f, 0.288f, -0.042f), 0.000f), new HandJoint(new Vec3(0.131f, -0.251f, -0.164f), new Quat(0.188f, -0.436f, 0.844f, -0.248f), 0.000f) };

Input.HandOverride(Handed.Right, joints);
```

# Working with Materials

Materials describe the visual appearance of everything on-screen, so having
a solid understanding of how they work is important to making a good
looking application! Fortunately, StereoKit comes with some great tools
built-in, and Materials can be a _lot_ of fun to work with!

## Using Materials

We've already seen that we can use a Material like this:
```csharp
Mesh.Sphere.Draw(Material.Default, Matrix.Identity);
```
This uses the primary default Material, which is a simple but
extremely fast and flexible Material. The default is great, but
not very interesting, it's just a white matte
surface! If we want it to look different, we'll have to change some
of the Material's parameters.

Before we can change the Material's parameters, I'd like to
point out an important fact! StereoKit does not draw objects
immediately when Draw is called: instead, it stores draw
information, and at the end of the frame it will sort, cull, and
batch everything, and _then_ draw it all at once! Since a Material
is a shared Asset, Meshes are drawn with the Material as it appears
at the end of the frame!

This means you **cannot** take one Material, modify it, draw,
modify it again, draw, and expect them to look different. Both
draw calls share the same Material Asset, and will look the same.
Instead, you _must_ make a new Material for each visually distinct
surface. Here's what that looks like:

### Material from Copy
```csharp
Material newMaterial;

void InitNewMaterial()
{
	// Start by just making a duplicate of the default! This creates a new
	// Material that we're free to change as much as we like.
	newMaterial = Material.Default.Copy();

	// Assign an image file as the primary color of the surface.
	newMaterial[MatParamName.DiffuseTex] = Tex.FromFile("floor.png");

	// Tint the whole thing greenish.
	newMaterial[MatParamName.ColorTint] = Color.HSV(0.3f, 0.4f, 1.0f);
}
void StepNewMaterial()
{
	Mesh.Sphere.Draw(newMaterial, Matrix.T(0,-3,0));
}
```
![Working with Material copies]({{site.screen_url}}/Materials_NewMaterial.jpg)
_It's uh... not the most glamorous material!_

Not all Materials will have the same parameters, and in fact,
parameters can vary wildly from Material to Material! This comes from
the Shader code that each Material has embedded at its core. The
Shader runs on the GPU, describes how each vertex is projected onto the
screen, and calculates the color of every pixel. Since each shader
program is different, each one has different parameters it works with!

While [`MatParamName`]({{site.url}}/Pages/Reference/MatParamName.html)
helps to codify and standardize common parameter names, it's always
best to be somewhat familiar with the Shader that the Material is
using.

For example, Material.Default uses [this Shader](https://github.com/StereoKit/StereoKit/blob/master/StereoKitC/shaders_builtin/shader_builtin_default.hlsl),
and you can see the parameters listed at the top:
```csharp
//--color:color = 1,1,1,1
//--tex_scale   = 1
//--diffuse     = white

float4    color;
float     tex_scale;
Texture2D diffuse : register(t0);
```
Shaders use data embedded in comments to assign default values to
material properties, the `//--` indicates this. So in this case,
`color` is a float4 (Vec4 or Color in C#), with a default value of
`1,1,1,1`, white. This maps to [`MatParamName.ColorTint`]({{site.url}}/Pages/Reference/MatParamName.html),
but you could also use the name directly:
`newMaterial["color"] = Color.HSV(0.3f, 0.2f, 1.0f);`.

Materials also have a few properties that aren't part of the Shader,
things like [depth testing]({{site.url}}/Pages/Reference/Material/DepthTest.html)/[writing]({{site.url}}/Pages/Reference/Material/DepthWrite.html),
[transparency]({{site.url}}/Pages/Reference/Material/Transparency.html),
[face culling]({{site.url}}/Pages/Reference/Material/FaceCull.html),
or [wireframe]({{site.url}}/Pages/Reference/Material/Wireframe.html).

### Material from Shader

You can also create a completely new Material directly from a Shader!
StereoKit does keep the default Shaders around in the [`Shader`]({{site.url}}/Pages/Reference/Shader.html)
class for this purpose, but you can also use Shader.FromFile to load a
pre-compiled shader file, and use that instead. More on that in the
[Shader guide (coming soon)]().
```csharp
Material shaderMaterial;

void InitShaderMaterial()
{
	// Instead of copying Material.Default, we're creating a completely new
	// Material directly from a Shader.
	shaderMaterial = new Material(Shader.Default);

	// Make it just slightly transparent
	shaderMaterial.Transparency = Transparency.Blend;
	shaderMaterial[MatParamName.ColorTint] = new Color(1, 1, 1, 0.9f);
}
void StepShaderMaterial()
{
	Mesh.Sphere.Draw(shaderMaterial, Matrix.T(0,2,0));
}
```
![Material from a Shader]({{site.screen_url}}/Materials_ShaderMaterial.jpg)
_It's a spooky circle now._
## Environment and Lighting

StereoKit's default lighting system is entirely based on environment
lighting! This can drastically affect how a material looks, so choosing the
right lighting can make a big difference in how your content looks. Here's
a simple white sphere again, but with a more complex lighting than the
default white room.

![Interesting lighting]({{site.screen_url}}/MaterialDefault.jpg)

You can change the environment lighting with a nice cubemap, check out the
[`Renderer.SkyLight`]({{site.url}}/Pages/Reference/Renderer/SkyLight.html)
property for a nice example of how to do this!

## Materials and Performance

Since Materials are responsible for drawing everything on the screen, they
have a big impact on GPU side performance! If you check your device's
performance monitor and see the GPU maxed out at 100% all the time, it's a
good moment to take a peek at how you're working with Materials.

The first rule is that fewer Materials means better GPU utilization. GPUs
don't like switching between Shaders or even Material parameters, so if you
can re-use a Material safely, you should! StereoKit does a great job of
batching draw calls together to reduce this switching, but this is only
effective at boosting performance if Materials are getting re-used.

The next rule is that simpler Shaders are faster. Material.Unlit is just
about the fastest Material you can have, followed closely by
Material.Default! Material.PBR looks great, but does a lot of work to look
good. It's very fast compared to many other PBR shaders, and quite suitable
even on mobile VR headsets, but if you don't need it, use something faster!

And lastly, small textures are faster than large ones. Textures get sampled
a lot during rendering, which means moving around lots of texture memory!
Remember that halving a texture's size can reduce memory by a factor of 4!

It often helps to just see how long a draw call takes! For this, I like to
use [RenderDoc](https://renderdoc.org/)'s timing feature. RenderDoc works
quite nicely with StereoKit's flatscreen mode, and while this isn't a
perfect representation of performance on mobile devices, it's a solid
reference point.

## A Look at the Defaults

StereoKit strives to cover the basics for you, and Materials are no
exception! You'll find a collection of Materials and Shaders that are
designed to be performant and good looking on mobile XR headsets, and
should cover the majority of use-cases. Here's a sampling, and check
the docs for each one to see what properties they support!

### [`Material.Default`]({{site.url}}/Pages/Reference/Default/Material.html)
![Material.Default preview]({{site.screen_url}}/MaterialDefault.jpg)

### [`Material.Unlit`]({{site.url}}/Pages/Reference/Default/MaterialUnlit.html)
![Material.Unlit preview]({{site.screen_url}}/MaterialUnlit.jpg)

### [`Material.PBR`]({{site.url}}/Pages/Reference/Default/MaterialPBR.html)
![Material.PBR preview]({{site.screen_url}}/MaterialPBR.jpg)

### [`Material.UI`]({{site.url}}/Pages/Reference/Default/MaterialUI.html)
![Material.UI preview]({{site.screen_url}}/MaterialUI.jpg)

### [`Material.UIBox`]({{site.url}}/Pages/Reference/Default/MaterialUIBox.html)
![Material.UIBox preview]({{site.screen_url}}/MaterialUIBox.jpg)
