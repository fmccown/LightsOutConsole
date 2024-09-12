using LightsOutConsole;

namespace LightsOutTestProject
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void Test_DefaultGridSize()
        {
            LightsOutGame game = new LightsOutGame();
            Assert.AreEqual(3, game.GridSize);
        }

        [TestMethod]
        public void Test_ValidGridSizes()
        {
            LightsOutGame game = new LightsOutGame();
            for (int gridSize = 3; gridSize <= 7; gridSize++)
            {
                game.GridSize = gridSize;
                Assert.AreEqual(gridSize, game.GridSize);
            }
        }

        [TestMethod]
        public void Test_InvalidGridSizeThrowsException()
        {
            LightsOutGame game = new LightsOutGame();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => game.GridSize = 2);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => game.GridSize = 8);
        }

        [TestMethod]
        public void Test_NewGame()
        {
            LightsOutGame game = new LightsOutGame();
            string gridState = game.ToString();
            game.NewGame();
            string newGridState = game.ToString();

            Assert.AreNotEqual(gridState, newGridState);
        }

        [TestMethod]
        public void Test_IsOn()
        {
            LightsOutGame game = new LightsOutGame();
            game.NewGame();

            // Remove eoln from grid state
            string gridState = game.ToString().Replace(Environment.NewLine, "");

            int index = 0;
            for (int r = 0; r < game.GridSize; r++)
            {
                for (int c = 0; c < game.GridSize; c++)
                {
                    Assert.AreEqual(gridState[index] == 'T', game.IsOn(r, c),
                        $"Wrong value at ({r}, {c})");
                    index++;
                }
            }
        }

        [TestMethod]
        public void Test_FlipLight()
        {
            // Index locations that would result in flipped lights
            var lightsFlipped = new List<HashSet<int>>
            {
                new HashSet<int>() { 0, 1, 3 },        // 0
                new HashSet<int>() { 0, 1, 2, 4 },     // 1
                new HashSet<int>() { 1, 2, 5 },        // 2
                new HashSet<int>() { 0, 3, 4, 6 },     // 3
                new HashSet<int>() { 1, 3, 4, 5, 7 },  // 4
                new HashSet<int>() { 2, 4, 5, 8 },     // 5
                new HashSet<int>() { 3, 6, 7 },        // 6
                new HashSet<int>() { 4, 6, 7, 8 },     // 7
                new HashSet<int>() { 5, 7, 8 }         // 8
            };

            LightsOutGame game = new LightsOutGame();
            game.NewGame();
            string gridState = game.ToString().Replace(Environment.NewLine, "");

            int flipLightIndex = 0;
            for (int r = 0; r < game.GridSize; r++)
            {
                for (int c = 0; c < game.GridSize; c++)
                {
                    game.FlipLight(r, c);
                    string newGridState = game.ToString().Replace(Environment.NewLine, "");

                    var indexesFlipped = lightsFlipped[flipLightIndex];

                    // Verify the differences between gridState and newGridState
                    // are contained in indexesFlipped
                    for (int i = 0; i < newGridState.Length; i++)
                    {
                        int indexRow = i / game.GridSize;
                        int indexCol = i % game.GridSize;

                        // Check if this light should be flipped or not
                        if (newGridState[i] == gridState[i])
                        {
                            Assert.IsFalse(indexesFlipped.Contains(i),
                                $"Light at ({indexRow}, {indexCol}) should be flipped when " +
                                $"flipping light at ({r}, {c})");
                        }
                        else
                        {
                            Assert.IsTrue(indexesFlipped.Contains(i),
                               $"Light at ({indexRow}, {indexCol}) should NOT be flipped when " +
                               $"flipping light at ({r}, {c})");
                        }
                    }

                    // Return to original state
                    game.FlipLight(r, c);

                    flipLightIndex++;
                }
            }
        }

        [TestMethod]
        public void Test_ToString()
        {
            LightsOutGame game = new LightsOutGame();
            game.NewGame();

            string gridState = game.ToString();

            // Should contain 2 newlines at correct locations
            Assert.AreEqual(3, gridState.IndexOf(Environment.NewLine));
            Assert.AreEqual(6 + Environment.NewLine.Length,
                gridState.IndexOf(Environment.NewLine, 5));

            gridState = gridState.Replace(Environment.NewLine, "");

            // String should only contain T/F 
            for (int i = 0; i < gridState.Length; i++)
            {
                Assert.IsTrue(gridState[i] == 'T' || gridState[i] == 'F');
            }
        }


        [TestMethod]
        public void Test_Cheat()
        {
            for (int gridSize = 3; gridSize <= 7; gridSize++)
            {
                LightsOutGame game = new LightsOutGame
                {
                    GridSize = gridSize
                };

                game.Cheat();

                for (int r = 0; r < game.GridSize; r++)
                {
                    for (int c = 0; c < game.GridSize; c++)
                    {
                        // Only 3 lights in upper-left corner should be on
                        if (r == 0 && c == 0 ||
                            r == 0 && c == 1 ||
                            r == 1 && c == 0)
                        {
                            Assert.IsTrue(game.IsOn(r, c));
                        }
                        else
                        {
                            Assert.IsFalse(game.IsOn(r, c));
                        }
                    }
                }
            }
        }
    }
}