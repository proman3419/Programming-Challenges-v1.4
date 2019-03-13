import requests
import json
from datetime import datetime
from datetime import timedelta


def get_input():
  while True:
    utc = input('Pass me an UTC offset ')

    try:
      utc = int(utc)
      if -12 <= utc <= 12:
        time = request_time()
        result = apply_offset(time, utc)
        print(result)
        return
      else:
        print('The input should be >= -12 and <= 12')
    except:
      print('The input should be an integer')


def request_time():
  response = requests.get('http://worldclockapi.com/api/json/utc/now')
  # Convert from 2019-03-13T11:52Z to 2019-03-13 11:52
  return json.loads(response.text)['currentDateTime'][:-1].replace('T', ' ')


def apply_offset(time, utc):
  d = timedelta(hours=utc)
  time = datetime.strptime(time, '%Y-%m-%d %H:%M')
  time = time + d
  time = time.strftime('%Y-%m-%d %H:%M')
  return time


def main():
  get_input()


if __name__ == '__main__':
  main()