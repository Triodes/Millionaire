using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Millionaire.Objects;
using System;
using System.Collections.Generic;
using System.IO;

namespace Millionaire.States
{
    class QuestionState : ObjectRoot
    {
        AnswerObject[] answers = new AnswerObject[4];
        TextBoxObject question;

        int state = 0;
        AnswerObject selectedAnswer = null;
        int correctAnswer = -1;
        Question[] questions = new Question[15];

        HelpWindowObject helpWindow;

        public QuestionState(Millionaire game) : base(game, "questionstate")
        {
            StreamReader fileReader = new StreamReader("Questions.txt");

            for (int i = 0; i < 15; i++)
            {
                questions[i] = new Question(fileReader);
                fileReader.ReadLine();
            }
            fileReader.Close();

            SpriteGameObject logo = new SpriteGameObject("logo", "Images/logo");
            logo.width = (int)(0.6f * logo.width);
            logo.height = (int)(0.6f * logo.height);
            logo.Position = new Vector2(Millionaire.Resolution.X / 2f - logo.width / 2f, 0);
            Add(logo);

            SpriteGameObject line1 = new SpriteGameObject("logo", "Images/whitepixel");
            line1.width = Millionaire.Resolution.X;
            line1.height = 3;
            line1.Position = new Vector2(0, 369);
            Add(line1);

            SpriteGameObject line2 = new SpriteGameObject("logo", "Images/whitepixel");
            line2.width = Millionaire.Resolution.X;
            line2.height = 3;
            line2.Position = new Vector2(0, 489);
            Add(line2);

            SpriteGameObject line3 = new SpriteGameObject("logo", "Images/whitepixel");
            line3.width = Millionaire.Resolution.X;
            line3.height = 3;
            line3.Position = new Vector2(0, 609);
            Add(line3);

            question = new TextBoxObject("question", Alignment.Center, "Images/question");
            question.height = 100;
            question.width = 1000;
            question.finalPosition = new Vector2(Millionaire.Resolution.X / 2 - question.width / 2, 320);
            question.Position = new Vector2(-1200, 320);
            Add(question);

            // Answers

            for (int i = 0; i < 4; i++)
            {
                AnswerObject answer = new AnswerObject("answer_" + i.ToString(), i);
                float aScale = 124f / answer.height;
                answer.height = 100;
                answer.width = 475;
                if (i % 2 == 0)
                {
                    answer.finalPosition = new Vector2(183, 440 + i / 2 * 120);
                    answer.Position = new Vector2(-600, 440 + i / 2 * 120);
                }
                else
                {
                    answer.finalPosition = new Vector2(Millionaire.Resolution.X - answer.width - 183, 440 + i / 2 * 120);
                    answer.Position = new Vector2(1491, 440 + i / 2 * 120);
                }
                Add(answer);
                answers[i] = answer;
            }

            helpWindow = new HelpWindowObject(game);
            Add(helpWindow);
        }

        private void Init()
        {
            for (int i = 0; i < 4; i++)
            {
                 answers[i].Clear();
                answers[i].SetText(questions[game.scoreState.score].answers[i]);
                if (i % 2 == 0)
                    answers[i].Position = new Vector2(-600, answers[i].Position.Y);
                else
                    answers[i].Position = new Vector2(1491, answers[i].Position.Y);

            }
            question.Clear();
            question.SetText(questions[game.scoreState.score].question);
            question.Position = new Vector2(-1200, question.Position.Y);

            state = 0;
            selectedAnswer = null;
            correctAnswer = questions[game.scoreState.score].correctAnswer;
            helpWindow.active = false;
        }

        public override void Update(GameTime gameTime, InputHelper inputHelper)
        {
            if (!helpWindow.active)
                UpdateBase(inputHelper);

            base.Update(gameTime, inputHelper);
        }

        private void UpdateBase(InputHelper inputHelper)
        {
            if (inputHelper.IsKeyPressed(Keys.Space) && !activatedThisTick)
            {
                if (state < 4)
                    answers[state].Animate();

                if (state == 1)
                {
                    string song = "1001000 music";
                    if (game.scoreState.score > 5)
                        song = "200032000";
                    if (game.scoreState.score > 11)
                        song = "64000 music";

                    Millionaire.ResourceManager.PlayMusic("Sound/" + song, true);
                }

                if (state == 5)
                {
                    if (selectedAnswer.ansNumber == correctAnswer)
                    {
                        selectedAnswer.Correct();
                        Millionaire.ResourceManager.PlayMusic("Sound/correct answer");
                        game.scoreState.score++;
                    }
                    else
                    {
                        selectedAnswer.False();
                        answers[correctAnswer].Correct();
                        Millionaire.ResourceManager.PlayMusic("Sound/wrong answer");
                    }


                }

                if (state == 6)
                {
                    if (selectedAnswer.ansNumber == correctAnswer && !game.scoreState.AllCorrect())
                        game.SwitchRoot("score");
                    else
                        game.SwitchRoot("endgame");
                }


                if (state != 4)
                    state++;
            }

            if (state == 4)
            {
                if (inputHelper.IsKeyPressed(Keys.A) && answers[0].canPick)
                {
                    selectedAnswer = answers[0].Select();
                    state++;
                }

                if (inputHelper.IsKeyPressed(Keys.B) && answers[1].canPick)
                {
                    selectedAnswer = answers[1].Select();
                    state++;
                }

                if (inputHelper.IsKeyPressed(Keys.C) && answers[2].canPick)
                {
                    selectedAnswer = answers[2].Select();
                    state++;
                }

                if (inputHelper.IsKeyPressed(Keys.D) && answers[3].canPick)
                {
                    selectedAnswer = answers[3].Select();
                    state++;
                }

                if (inputHelper.IsKeyPressed(Keys.H) && !helpWindow.active)
                    helpWindow.active = true;

                if (state == 5)
                    Millionaire.ResourceManager.PlayMusic("Sound/final answer");
            }
        }

        public void DoFiftyFifty()
        {
            List<int> wrongAnswers = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                if (i != correctAnswer)
                    wrongAnswers.Add(i);
            }

            int randVal = (int)((new Random()).NextDouble()*3d);
            wrongAnswers.RemoveAt(randVal);

            foreach (int wrongAnswer in wrongAnswers)
            {
                answers[wrongAnswer].Visible = false;
                answers[wrongAnswer].canPick = false;
            }
        }

        public override void Activate()
        {
            base.Activate();
            Millionaire.ResourceManager.PlayMusic("Sound/lets play", false);
            Init();
            question.Animate();
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }
    }
}
