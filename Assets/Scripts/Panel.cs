/**
 * Panel interface
 * 
 * @version 1.0.0, new Interface
 * @author S3
 * @date 2024/03/08
*/

public interface Panel
{
    void Init();
    void SetPanel(bool OnOff); // Set panel
    bool GetPanelIsOn(); // Return is panel on or off
    void EscListener(); // Esc key's listener
}