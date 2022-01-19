# Table-Tennis-Agent
## Aim: 
To create an intelligent agent that can play Table Tennis.

## Game Overview:
Dual Agent:
Two intelligent agents playing against each other in a singles table tennis match.  Both are attempting to win the game while following basic table tennis rules. The agent with the highest accumulated score wins the match.

Single Agent:
An intelligent agent learning to return serves from a serving bot following singles table tennis match rules. The agent has to learn to hit the ball onto the other side of court with increasing levels of difficulties of incoming serves. A higher score indicates better abilities to handle different varieties of serves.

## Website: https://sowmyavoona96.github.io/csci527/

## Installation Steps:
1. Install Unity: 2020.3.20f1 version
2. Install Python: 3.8.1
3. Install <a href="https://github.com/Unity-Technologies/ml-agents/blob/main/docs/Installation.md#install-the-comunityml-agents-unity-package"> com.unity.ml-agents </a>
4. Install mlagents python package: 
   ```
   python -m pip install mlagents==0.27.0
   ```
5. Clone this repository and open the project folder <a href="https://github.com/USC-CSCI527-Fall2021/Table-Tennis-Agent/tree/master/projects/ttagent"> ttagent </a> on unity.

## Environment:
1. Dual agent setup is in SampleScene.unity and the corresponding scripts are in Assets/scripts
2. Single agent setup is in ServeBotScene.unity and the corresponding scripts are in Assets/scripts/singleAgent

## Training:
To train an agent, set the model to none, behavior type to default under behavior parameters.

## Dual Agent:
1. To train the dual agent set up with SAC or PPO model:
   1. Switch to master branch
   2. Open <a href = "https://github.com/USC-CSCI527-Fall2021/Table-Tennis-Agent/blob/master/projects/ttagent/Assets/Scenes/SampleScene.unity"> SampleScene.unity</a>
   3. Run the following command in terminal, replace <model-name> with sac or ppo.
    ```
    mlagents-learn ttagent/Assets/config/<model-name>/TT.yaml --run-id=<any_name_to_store_this_training>
    ```
 
    ### Curriculum Learning:
2. To train the dual agent set up with curriculum learning over reward policy:
    Switch to curr_reward branch and run the same command as shown above.
   
3. To train the dual agent set up with curriculum learning over bat size scaling:
    Switch to curr_scale branch and run the same command as shown above.
  
## Single Agent:
1.  To train the single agent set up with SAC or PPO model:
      1. Switch to one of the below branches for corresponding type of serve difficulty:
   <ul>
      <li> <a href="https://github.com/USC-CSCI527-Fall2021/Table-Tennis-Agent/tree/single_agent">single_agent</a> for basic setup with fixed velocity and fixed     height </li>
      <li> <a href="https://github.com/USC-CSCI527-Fall2021/Table-Tennis-Agent/tree/single_agent_vel">single_agent_vel</a> for increased difficulty in serves with randomized velocity </li>
      <li> <a href="https://github.com/USC-CSCI527-Fall2021/Table-Tennis-Agent/tree/single_agent_spin">single_agent_spin</a> for increased difficulty in serves with randomized spins  </li>
  </ul> 
      2. Open ServeBotScene.unity.
      3. Run the following command, replace <model-name> with sac or ppo.
   ```
   mlagents-learn ttagent/Assets/config/singleAgent/<model-name>/TT.yaml --run-id=<any_name_to_store_this_training>
   ```
   ### Curriculum Learning:
2. To train the single agent set up with curriculum learning over reward policy:
   Switch to <a href="https://github.com/USC-CSCI527-Fall2021/Table-Tennis-Agent/tree/single_agent_curr_reward">single_agent_curr_reward</a> and run the same command as shown above.

## Viewing Results on Tensorboard:
  To view the results of a trained model run the following command:
  ```
  tensorboard --logdir results
  ```
  
## To play the game with a trained Model:
  Set agent's model to any trained model and behavior type to inference and click play to run the game.
