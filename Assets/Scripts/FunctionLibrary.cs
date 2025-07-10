using UnityEngine;

// Import Mathf methods statically for cleaner syntax (e.g., use Sin instead of Mathf.Sin)
using static UnityEngine.Mathf;

public static class FunctionLibrary
{
    // Delegate type for functions that take (x, t) and return y (float)
    public delegate Vector3 Function(float u, float v, float t);

    // enum of function names for dropdown
    public enum FunctionNames { Wave, MultiWave, Ripple, Sphere, Torus }

    // Array of functions
    static Function[] functions = { Wave, MultiWave, Ripple, Sphere, Torus };

    // Selects and returns a function based on the index value
    public static Function GetFunction(FunctionNames name)
    {
        return functions[(int)name];
    }

    // A basic sine wave: y = sin(π * (x + t))
    public static Vector3 Wave(float u, float v, float t)
    {
        Vector3 p;
        p.x = u;
        p.y = Sin(PI * (u + v + t));
        p.z = v;

        return p;
    }

    // A composite wave made of two sine waves at different frequencies and amplitudes
    public static Vector3 MultiWave(float u, float v, float t)
    {
        Vector3 p;
        p.x = u;
        p.y = Sin(PI * (u + 0.5f * t));        // Slower sine wave
        p.y += 0.5f * Sin(2f * PI * (v + t));        // Faster sine wave with lower amplitude
        p.y += Sin(PI * (u + v + 0.25f * t));
        p.y *= 1f / 2.5f;
        p.z = v;
        return p;                      // Normalize amplitude to stay within [-1,1]
    }

    // A ripple wave: wave that moves outward with diminishing amplitude based on distance
    public static Vector3 Ripple(float u, float v, float t)
    {
        float d = Sqrt(u * u + v * v);                          // Distance from center (origin)
        Vector3 p;
        p.x = u;
        p.y = Sin(PI * (4f * d - t));          // Phase shift makes wave appear to move outward
        p.y /= 1f + 10f * d;
        p.z = v;
        return p;           // Dampen amplitude as distance increases
    }

    public static Vector3 Sphere(float u, float v, float t)
    {
        float r = 0.9f + 0.1f * Sin(PI * (6f * u + 4f * v + t));
        float s = r * Cos(PI * 0.5f * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r * Sin(PI * 0.5f * v);
        p.z = s * Cos(PI * u);
        return p;
    }

    public static Vector3 Torus(float u, float v, float t)
    {
        float r1 = 0.7f + 0.1f * Sin(PI * (6f * u + 0.5f * t));
        float r2 = 0.15f + 0.05f * Sin(PI * (8f * u + 4f * v + 2f * t));
        float s = r1 + r2 * Cos(PI * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r2 * Sin(PI * v);
        p.z = s * Cos(PI * u);
        return p;
    }
}
