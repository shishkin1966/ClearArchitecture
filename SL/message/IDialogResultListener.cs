namespace ClearArchitecture.SL
{
    public interface IDialogResultListener : IValidated
    {
        void OnDialogResult(DialogResultAction action);
    }
}
