---
layout: default
title: Getting started
---

# Getting started with **NetLogo**

## Quickstart Tutorial

## **Step 1: Configure Library Properties**
1. Open the SyncroSim GUI and select **File I New**.
2. Select the **netlogo** base package and the **NetLogo Example** template; specify a file name and folder location for your netlogo SyncroSim library and click **OK**.
3. The [SyncroSim library explorer](http://docs.syncrosim.com/how_to_guides/library_explorer_overview.html) should now display your new netlogo library.
4. Highlight your netlogo library in the library explorer and select **File I Library Properties**.
5. On the **Options I NetLogo** of the **Library Properties** specify the **NetLogo executable filename** where the NetLogo.exe file is located. For the x64 version of NetLogo This is typically "**C:\Program Files\NetLogo 6.X.X**".
6. In the **Library Properties** for NetLogo you can also select to **Run in window** which will at runtime show the NetLogo GUI. This is useful for initially debugging your model but can be cumbersome if you are running many model realizations and scenarios.

## **Step 2: View Definitions**
1. In the [SyncroSim library explorer](http://docs.syncrosim.com/how_to_guides/library_explorer_overview.html) right click on **Definitions** and select the **Symbols** tab. The netlogo template library has been preloaded with a very simple example agent based model of biocontrol agents or **bugs** looking for and eating an invasive plant on the landscape. The model is written in a .nlogo code file that ships with the template library. In this file certain variables defined in the code can be set in the SyncroSim user interface by using **Symbols**. A **Symbol** is simply a collection of characters that denotes where a specific value should be inserted into the nlogo code file at runtime. Symbols can be used to set state variables. Symbols can also be used to set input files that need to be loaded by NetLogo at runtime such as a raster specifying the starting landscape conditions. Details on how **Symbols** are used in the NetLogo code files will be explained in the documentation section on **NetLogo Code**.
2. The last tab in the **Definitions** for the netlogo package is for **Output Variables**. The example library contains four output variables: **Closed**, **Open**, and **Invaded** are used to denote the amount of area in these three possible discrete vegetation states. The output variable **landCover** is used to represent a map of the landscape state. Like Symbols, Output Variables are used in the NetLogo code; their purpose is to allow SyncroSim to load NetLogo output into the model database and to display that output in the user interface. Further explanation on Output Variables is given in the section below on **NetLogo Code**.

## **Step 3: View Scenario Properties**
1. In the [SyncroSim library explorer](http://docs.syncrosim.com/how_to_guides/library_explorer_overview.html) right click on the scenario called **5 Bugs** and select **Open**. This is a scenario used to represent 5 biocontrol agents or **Bugs** moving across the landscape and eating **Invaded** patches.
2. The **Run Control** property for this scenario is set to run 10 timesteps and 7 iterations. The timesteps will be used to define how many [NetLogo ticks](http://ccl.northwestern.edu/netlogo/docs/dictionary.html#tick) to run each iteration for. The NetLogo model will be run once per iteration.
3. The **Script** property points to the [netLogoExample.nlogo](https://github.com/ApexRMS/netlogo/blob/master/src/Templates/netlogo-example.ssim.input/Scenario-1/netlogo_Script/netLogoExample.nlogo) code file that comes with the template library. The **Script** property also denotes the NetLogo **Experiment name** which is "experiment" by default in NetLogo. For more details on NetLogo experiments see the [NetLogo Documentation](https://ccl.northwestern.edu/netlogo/2.0.0/docs/behaviorspace.html) on Behaviour Space.
4. On the **Inputs** property note that you can specify a starting number of **Bugs** for the simulation.
5. On the **Input Files** property you can specify the input ascii raster file that denotes the starting conditions for the landscape.

## **Step 4: Run the NetLogo Model**
1. In the [SyncroSim library explorer](http://docs.syncrosim.com/how_to_guides/library_explorer_overview.html) right click on the scenario called **5 Bugs** and select **Run**.
2. If you checked **Run in window** in the **Options I NetLogo** library property, you will see the NetLogo GUI open and you will need to press **setup** then **go** followed by **File I Quit**. This will be repeated for every model realization.

## **Step 5: View Charts of Output**
Once the simulation is complete you can use the [SyncroSim chart window](http://docs.syncrosim.com/how_to_guides/results_chart_window.html) to view NetLogo outputs. Note that you can disaggregate and include output for any of the variables defined in the project **Definitions**. You can also choose to use the [rsyncrosim package](https://syncrosim.com/r-package/) to extract model output into R dataframes.

## **Step 6: View Maps of Output**
The example library also contains a map that displays the Land Cover (Open, Closed or Invaded) for each cell at different realizations and time points. Mapping variables can be either discrete such as in this example or continuous. To view this map in the SyncroSim GUI you will need to use the [SyncroSim map window](http://docs.syncrosim.com/how_to_guides/results_map_window.html).

## Build Your Own Model
For further information on the **netlogo** package, including instructions on how to build your own SyncroSim netlogo model, visit the [netlogo repository](https://github.com/ApexRMS/netlogo).
