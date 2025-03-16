import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:ums_app/blocs/auth_bloc.dart';
import 'package:ums_app/repositories/auth_repository.dart';
import 'package:ums_app/screens/splash_screen.dart';

void main() {
  runApp(const MainApp());
}

class MainApp extends StatelessWidget {
  const MainApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'UMS',
      theme: ThemeData(primarySwatch: Colors.blue),
      home: BlocProvider(create: (context) => AuthBloc(authRepository: AuthRepository()), child: const SplashScreen()),
    );
  }
}
