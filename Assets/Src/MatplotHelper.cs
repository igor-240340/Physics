using System.Collections.Generic;
using UnityEngine;

public static class MatplotHelper
{
    public static List<Particle> particles = new();
    private static List<Vector3> positionsAfterIntegrating = new();
    private static Dictionary<int, Vector3[]> preservedContacts = new();

    public static void DrawParticles(List<Particle> particles, string color = "black")
    {
        if (particles.Count == 0)
            return;

        List<float> x = new(), y = new();
        particles.ForEach(p =>
        {
            x.Add(p.pos.x);
            y.Add(p.pos.y);
        });

        MyPlot.Scatter(
            x.ToArray(), y.ToArray(), particles.Count,
            new[] {new MyPlot.KeywordValue("color", color)}, 1,
            15.0f);
    }
    
    public static void DrawParticles(string color = "black")
    {
        List<float> x = new(), y = new();
        particles.ForEach(p =>
        {
            x.Add(p.pos.x);
            y.Add(p.pos.y);
        });

        MyPlot.Scatter(
            x.ToArray(), y.ToArray(), particles.Count,
            new[] {new MyPlot.KeywordValue("color", color)}, 1,
            15.0f);
    }

    public static void DrawGens(List<IParticleContactGenerator> gens, bool label = true, string color = "forestgreen")
    {
        List<float> x = new(), y = new();

        gens.ForEach(g =>
        {
            List<float> x = new(), y = new();

            ParticleLink link = (ParticleLink) g;
            x.AddRange(new[] {link.particleA.pos.x, link.particleB.pos.x});
            y.AddRange(new[] {link.particleA.pos.y, link.particleB.pos.y});

            MyPlot.Plot(x.ToArray(), y.ToArray(), 2,
                new[]
                {
                    new MyPlot.KeywordValue("color", color),
                    new MyPlot.KeywordValue("label", link.CurrentLength().ToString())
                }, 2);
        });

        if (label)
            MyPlot.LabelLines();
    }

    public static void CopyPositionsOf(List<Particle> particles)
    {
        particles.ForEach(p => positionsAfterIntegrating.Add(p.pos));
    }

    public static void PreserveContactState(List<ParticleContact> contacts)
    {
        int contactIndex = 0;
        foreach (var contact in contacts)
        {
            preservedContacts.Add(contactIndex++, new[] {contact.particleA.pos, contact.particleB.pos});
        }
    }

    public static void DrawPreservedContact(int contactIndex)
    {
        Vector3 posA = preservedContacts[contactIndex][0];
        Vector3 posB = preservedContacts[contactIndex][1];
        DrawLine(posA, posB, "grey", false);
    }

    public static void DrawContact(ParticleContact contact, string color = "red", bool label = false)
    {
        Vector3 posA = contact.particleA.pos;
        Vector3 posB = contact.particleB.pos;
        DrawLine(posA, posB, color, label);
    }

    public static void DrawPosAfterIntegrating(string color = "grey")
    {
        List<float> x = new(), y = new();
        positionsAfterIntegrating.ForEach(p =>
        {
            x.Add(p.x);
            y.Add(p.y);
        });

        MyPlot.Scatter(
            x.ToArray(), y.ToArray(), positionsAfterIntegrating.Count,
            new[] {new MyPlot.KeywordValue("color", color)}, 1,
            15.0f);
    }

    public static void DrawLine(Vector3 posA, Vector3 posB, string color = "forestgreen", bool label = true)
    {
        List<float> x = new(), y = new();

        x.AddRange(new[] {posA.x, posB.x});
        y.AddRange(new[] {posA.y, posB.y});

        MyPlot.Plot(x.ToArray(), y.ToArray(), 2,
            new[]
            {
                new MyPlot.KeywordValue("color", color),
                new MyPlot.KeywordValue("label", (posA - posB).magnitude.ToString())
            }, 2);

        if (label)
            MyPlot.LabelLines();
    }

    public static void DrawVels(List<Particle> particles, string color = "blue")
    {
        if (particles.Count == 0)
            return;

        List<float> posX = new(), posY = new(), velX = new(), velY = new();
        particles.ForEach(p =>
        {
            posX.Add(p.pos.x);
            posY.Add(p.pos.y);

            velX.Add(p.velocity.x);
            velY.Add(p.velocity.y);
        });

        MyPlot.Quiver(
            posX.ToArray(), posY.ToArray(),
            velX.ToArray(), velY.ToArray(), particles.Count,
            new[]
            {
                new MyPlot.KeywordValue("units", "xy"),
                new MyPlot.KeywordValue("angles", "xy"),
                new MyPlot.KeywordValue("scale", "1"),
                new MyPlot.KeywordValue("scale_units", "xy"),
                new MyPlot.KeywordValue("color", color)
            }, 5);
    }

    public static void ClearPlot()
    {
        MyPlot.Clf();
        positionsAfterIntegrating.Clear();
        preservedContacts.Clear();
    }
}