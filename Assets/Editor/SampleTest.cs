﻿using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class SampleTest {

    [Test]
    public void SampleTestSimplePasses() {
        // Use the Assert class to test conditions.
        Assert.IsTrue(true);
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator SampleTestWithEnumeratorPasses() {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        yield return null;
    }
}
