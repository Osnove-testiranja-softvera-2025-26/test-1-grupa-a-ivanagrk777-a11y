using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS2026_GrupaA.Test
{
    internal class GameTest
    {
        [TestFixture]
        public class GameTest
        {
            private Game game;
            {
                [SetUp]
                public void SetUp()
            {
                game = new Game(
                new Position(0, 0, 0),
                new Position(10, 10, 10));
            }
            [TestCase(-1, 0, 0)]
            [TestCase(0, -1, 0)]
            [TestCase(0, 0, -1)]
            [TestCase(30, 0, 0)]
            [TestCase(0, 30, 0)]
            [TestCase(0, 0, 30)]
            public void Game_PlayerPositionOutsideMap_ThrowsException(int x, int y, int z)
            {
                Exception ex = Assert.Throws<PositionOutsideOffMapException>(()
                    =>
                new Game(new Position(x, y, z), new Position(10, 10, 10)));
                Assert.That(ex.Message, Is.EqualTo("position is outside of map");
            }
            [TestCase(-1, 0, 0)]
            [TestCase(0, -1, 0)]
            [TestCase(0, 0, -1)]
            [TestCase(30, 0, 0)]
            [TestCase(0, 30, 0)]
            [TestCase(0, 0, 30)]
            public void Game_RevealHiddenItemPositionOutsideMap_ThrowsException(int x, int y, int z)
            {
                Exception ex = Assert.Throws<PositionOutsideOffMapException>(()
                   =>
               new Game(new Position(0, 0, 0), new Position(x, y, z)));
                Assert.That(ex.Message, Is.EqualTo("position is outside of map");
            }
            [TestCase(0, 0, 0, true)]
            [TestCase(1, 1, 1, true)]
            [TestCase(29, 29, 29, true)]
            [TestCase(-1, 0, 0, false)]
            [TestCase(0, -1, 0, false)]
            [TestCase(0, 0, -1, false)]
            [TestCase(30, 0, 0, false)]
            [TestCase(0, 30, 0, false)]
            [TestCase(0, 0, 30, false)]
            public void ValidatePosition_PositionInsideOrOutsideMap_SuccessfulValidation(int x, int y, int z, bool expectedResult)
            {
                bool actualResult = game.ValidatePosition(new Position(x, y, z));
                Assert.AreEqual(expectedResult, actualResult);
            }
            [TestCase(5,5,0, false)]
            [TestCase(6,5, 0,false)]
            [TestCase(25,5,0, false)]
            public void ValidatePosition_PositionIsMapBarrier_ReturnsFalse(int x, int y, int z, bool expectedResult)
            {
                bool actualResult = game.ValidatePosition(new Position(x, y, z));
                Assert.AreEqual(expectedResult, actualResult);
            }
            [Test]
            public void MovePlayer_ValidMove_PlayerPositionChanged()
            {
                game.Player.Position = new Position(10, 10, 10);
                game.MovePlayer(Move.Up);
                Assert.AreEqual(11, game.Player.Position.Y);
            }
            [Test]
            public void MovePlayer_InvalidMoveOutsideMapp_PlayerPositionNotChanged()
            {
                game.Player.Position = new Position(0,0,0);
                game.MovePlayer(Move.Down);
                Assert.AreEqual(0, game.Player.Position.Y);
            }
            [Test]
            public void MovePlayer_InvalidMoveToBarrier_PlayerPositionNotChanged()
            {
                game.Player.Position = new Position(5, 4, 0);
                game.MovePlayer(Move.Up);
                Assert.AreEqual(4, game.Player.Position.Y);
            }
            [Test]
            public void CollectItems_PlayerOnGold_GoldCollected()
            {
                game.Player.Position = new Position(10, 10, 10);
                game.Space.AddTile(TileContent.Gold, TileTypsStandard, 10, 10, 10);
                game.CollectItems()
                    Assert.AreEqual(1, game.Player.StandardGold);
            }
            [Test]
            public void CollectItems_PlayerOnRevealHiddenItem_ItemColected()
            {
                game.Player.Position = new Position(10, 10, 10);
                game.Space.AddTile(TileContent.Reveal, HiddenItem, TileTypsStandard 10, 10, 10);
                game.CollectItems()
                    Assert.AreEqual(true, game.Player.HasRevealHiddenItem());
            }
            [Test]
            public void CollectItems_PlayerOnGold_TileEmptied()
            {
                game.Player.Position = new Position(10, 10, 10);
                game.Space.AddTile(TileContent.Reveal, HiddenItem, TileTypsStandard 10, 10, 10);
                game.CollectItems()
                    Assert.AreEqual(TileContent.Empty, game.space.Tiles[10, 10, 10].Content);
            }
            [TestCase(0, 0, false, Score.Bad)]
            [TestCase(15,0,true, Score.Bad)]
            [TestCase(16,0, false, Score.Bad)]
            [TestCase(16,0,true,Score.Average)]
            [TestCase(16,5,true,Score.Good )]
            [TestCase(0,11,false, Score.Good)]
            [TestCase(16,11,true, Score.Good)]
        }

        }
        }
        
}
