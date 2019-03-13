def get_input():
  while True:
    n = input('How many elements of Fibonacci sequence do you want to get? ')

    try:
      n = int(n)
      if n > 0:
        fibonacci(n)
      else:
        print('The input should be a positive integer')
    except:
      print('The input should be a positive integer')


def fibonacci(n):
  if n >= 1:
    print(1)
  if n >= 2:
    print(1)
  if n >= 3:
    a = b = 1
    for i in range(n - 2):
      c = a + b
      a, b = b, c
      print(c)


def main():
  get_input()


if __name__ == '__main__':
  main()