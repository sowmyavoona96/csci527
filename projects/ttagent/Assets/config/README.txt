For training using:
1. TT: In Unity Behavior name of the agent bats set to 'My Behavior'.
 
2. Bat A vs Bat B: In Unity change Behavior name of the agent bats from 'My Behavior' to 'Bat A' & 'Bat B' respectively for the two agent bats. 
--


Steps for training the agent:
1. Set up a Virtual environment.
2. In Unity change "Behavior Type" of agents to "Default" instead of "Heuristic" before training.
3. Run the command, mlagents-learn <path>/config/ppo/TT.yaml --run-id=firstTTRun. For each run the run-id needs to be unique.
4. Once training is complete, visualize the results in dashboard using the command, tensorboard --logdir results.