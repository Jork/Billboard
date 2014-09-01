interface Action {
	(): void;
}

interface Action1<TArg1> {
	(arg: TArg1): void;
}

interface Action2<TArg1, TArg2> {
	(arg1: TArg1, arg2: TArg2);
}

interface Func<TResult> {
	(): TResult;
}

interface Func1<TArg1, TResult> {
	(arg: TArg1): TResult;
}

interface Func2<TArg1, TArg2, TResult> {
	(arg1: TArg1, arg2: TArg2): TResult;
}