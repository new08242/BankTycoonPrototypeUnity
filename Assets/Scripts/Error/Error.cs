public class Error {
  public string errorMessage;

  public Error(string msg) {
    this.errorMessage = msg;
  }

  public string ToString() {
    return errorMessage;
  }
}