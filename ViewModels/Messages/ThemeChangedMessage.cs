using CommunityToolkit.Mvvm.Messaging.Messages;

namespace KafkaLens.ViewModels.Messages;

public class ThemeChangedMessage(string theme) : ValueChangedMessage<string>(theme);
