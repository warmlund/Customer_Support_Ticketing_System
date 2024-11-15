using Customer_Support_Ticketing_System_PL.Commands;

namespace Customer_Support_Ticketing_System_PL_Tests
{
    /// <summary>
    /// Unit test that tests event handling in the class Command
    /// The class inherits ICommand. Used for binding commands from the UI to the view model.
    /// </summary>
    public class CommandTest
    {
        private Command command;

        [SetUp]
        public void Setup()
        {
            //Creates a test command with a placeholder methods
            command = new Command(() => { }, () => true);
        }

        [Test]
        public void CommandSubscriptionTest()
        {
            //Arrange
            bool eventRaised = false;

            //Subscribe to the event
            command.CanExecuteChanged += (s, e) => eventRaised = true;

            //Act
            command.RaiseCanExecuteChanged();

            //Assert
            Assert.That(eventRaised, Is.True, "The CanExecuteChanged event should be raised");
        }

        [Test]
        public void CommandEventRaiseTest()
        {
            // Act & Assert
            //An exception will not be thrown because the method uses a null-propagating operation '?.'
            //which means that if there are no subscribers or the event handler is null nothing will happen
            //And no excpetion will be thrown. Therefor testing that no exception will be thrown

            Assert.DoesNotThrow(() => command.RaiseCanExecuteChanged(),
                "RaiseCanExecuteChanged should not throw an exception when there are no subscribers."); 
        } 
    }
}