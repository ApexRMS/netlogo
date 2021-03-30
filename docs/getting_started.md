---
layout: default
title: Getting started
---

# Getting started with **NetLogo**

## Quickstart Tutorial

## **Step 1: Configure Library Properties**
1. Open the SyncroSim GUI and select **File | New**.
2. Select the **netlogo** base package and the **NetLogo Example** template; specify a file name and folder location for your netlogo SyncroSim library and click **OK**.
3. The [SyncroSim library explorer](http://docs.syncrosim.com/how_to_guides/library_explorer_overview.html) should now display your new netlogo library.
4. Highlight your netlogo library in the library explorer and select **File | Library Properties**.
5. On the **Options | NetLogo** of the **Library Properties** specify the **NetLogo executable filename** where the NetLogo.exe file is located. For the x64 version of NetLogo This is typically "**C:\Program Files\NetLogo 6.X.X**".
6. In the **Library Properties** for NetLogo you can also select to **Run in window** which will at runtime show the NetLogo GUI. This is useful for initially debugging your model but can be cumbersome if you are running many model realizations and scenarios.

## **Step 2: View Definitions**
1. In the [SyncroSim library explorer](http://docs.syncrosim.com/how_to_guides/library_explorer_overview.html) right click on **Definitions** and select the **Symbols** tab. The netlogo template library has been preloaded with a very simple example agent based model of biocontrol agents or bugs looking for and eating an invasive plant on the landscape. The model is written in a .nlogo code file that ships with the template library. In this file certain variables defined in the code can be set in the SyncroSim user interface by using **Symbols**. A **Symbol** is simply a collection of characters that denotes where a specific value should be inserted into the nlogo code file at runtime. Symbols can be used to set state variables. Symbols can also be used to set input files that need to be loaded by NetLogo at runtime such as a raster specifying the starting landscape conditions. Details on how **Symbols** are used in the NetLogo code files will be explained in the documentation section on **NetLogo Code**.
2.
