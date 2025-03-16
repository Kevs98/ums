import 'package:flutter/material.dart';
import 'package:ums_app/screens/login_screen.dart';

class SplashScreen extends StatelessWidget {
  const SplashScreen({super.key});

  @override
  Widget build(BuildContext context) {
    Future.delayed(Duration(seconds: 3), () {
      Navigator.pushReplacement(context, MaterialPageRoute(builder: (context) => LoginScreen()));
    });

    return Scaffold(body: Center(child: FlutterLogo(size: 150)));
  }
}
