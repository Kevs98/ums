import 'package:flutter/material.dart';
import 'package:otp_text_field_v2/otp_field_v2.dart';

class OtpModal extends StatelessWidget {
  final Function(String) onOtpSubmitted;
  final String otpSecret;

  const OtpModal({required this.onOtpSubmitted, required this.otpSecret, super.key});

  @override
  Widget build(BuildContext context) {
    final otpController = OtpFieldControllerV2();

    return Padding(
      padding: EdgeInsets.only(bottom: MediaQuery.of(context).viewInsets.bottom),
      child: Container(
        padding: const EdgeInsets.all(24.0),
        decoration: BoxDecoration(color: Colors.white, borderRadius: BorderRadius.vertical(top: Radius.circular(24.0))),
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            Text(
              'Ingresa el código de verificación',
              style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
              textAlign: TextAlign.center,
            ),
            SizedBox(height: 20),
            Padding(
              padding: const EdgeInsets.symmetric(horizontal: 16.0),
              child: OTPTextFieldV2(
                controller: otpController,
                length: 6,
                width: MediaQuery.of(context).size.width,
                textFieldAlignment: MainAxisAlignment.spaceEvenly,
                fieldWidth: 48,
                fieldStyle: FieldStyle.box,
                outlineBorderRadius: 12,
                style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
                onChanged: (String otp) {},
                onCompleted: (String otp) {
                  onOtpSubmitted(otp);
                  Navigator.pop(context);
                },
              ),
            ),
            SizedBox(height: 20),
          ],
        ),
      ),
    );
  }
}
