import ftplib
from ftplib import FTP
import os

class Client():
  def __init__(self):
    self.cyan = lambda text: '\033[0;36m' + text + '\033[0m'
    self.green = lambda text: '\033[0;32m' + text + '\033[0m'

    self.user_n = ''
    self.server_n = ''
    self.directory_n = ''
    self.ftp_session = None

    self.update_command_prompt()
    self.client_loop()

  def update_command_prompt(self):
    if len(self.user_n) > 0 and len(self.server_n) > 0:
      self.command_prompt = self.cyan('{}@{}'.format(self.user_n, self.server_n)) + ':{}> '.format(self.green(self.directory_n))
    else:
      self.command_prompt = '> '

  def client_loop(self):
    while True:
      user_input = input(self.command_prompt).split() #'connect speedtest.tele2.net'.split()

      try:
        cmd = user_input[0]
        args = user_input[1:] if len(user_input) > 1 else None

        if cmd == 'connect': # connect
          self.connect(args[0]) # args[0] speedtest.tele2.net

          try:
            self.login(args[1], args[2])
          except:
            self.login()     

        elif cmd == 'ls':
          self.show_directory()

        elif cmd == 'cd':
          self.change_directory(args[0])

        elif cmd == 'mkdir':
          self.ftp_session.mkd(args[0])

        elif cmd == 'rmdir':
          self.ftp_session.rmd(args[0])

        elif cmd == 'rm':
          self.ftp_session.delete(args[0])

        elif cmd == 'post':
          self.post(args[0])

        elif cmd == 'get':
          self.get(args[0])

        elif cmd == 'exit':
          exit()

        self.update_command_prompt()

      except (Exception, ftplib.error_perm) as e:
        print(e)

  def connect(self, server_n):
    self.ftp_session = FTP(server_n)
    self.server_n = server_n

  def login(self, user='anonymous', passwd='anonymous'):
    self.ftp_session.login(user, passwd)
    self.user_n = user
    self.directory_n = self.ftp_session.pwd()
    print(self.ftp_session.getwelcome())

  def change_directory(self, directory_n):
    self.ftp_session.cwd(directory_n)
    self.directory_n = self.ftp_session.pwd()

  def show_directory(self):
    lines = []
    self.ftp_session.retrlines('LIST -a', lines.append)
    self.equalize_lines(lines)

  def equalize_lines(self, lines):
    elements = [x.split() for x in lines]
    e_in_l = len(elements[0])
    l_in_ls = len(lines)
    max_lens = [0 for i in range(e_in_l)]

    for i in range(e_in_l):
      for j in range(l_in_ls):
        curr_len = len(elements[j][i])

        if curr_len > max_lens[i]:
          max_lens[i] = curr_len

    _lines = ['' for i in range(l_in_ls)]

    for i in range(e_in_l):
      for j in range(l_in_ls):
        _lines[j] += (elements[j][i]).rjust(max_lens[i], ' ') + ' '

    print('\n'.join(_lines))

  def post(self, path):
    if os.path.exists(path):
      result = ''

      if os.path.isdir(path):
        for root, dirs, files in os.walk(path):
          for name in dirs:
            self.ftp_session.mkd(os.path.join(root, name))

          for name in files:
            with open(os.path.join(root, name), 'rb') as f:
              result = self.ftp_session.storbinary('STOR {}'.format(os.path.join(root, name)), f)

      else:
        with open(path, 'rb') as f:
          result = self.ftp_session.storbinary('STOR {}'.format(path), f)
      
      print(result)
    else:
      print('The file/directory doesn\'t exist')

  def get(self, path):
    try:
      self.get_file(path)
    except ftplib.error_perm as e:
      self.get_dir(path, e)

  def get_file(self, path):
    file_n = os.path.basename(path)
    curr_dir_n = os.path.dirname(os.path.realpath(__file__))

    handle = open(file_n, 'wb')
    result = self.ftp_session.retrbinary('RETR %s' % path, handle.write)
    handle.close()

    print(result)
    print('File saved as {}'.format(os.path.join(curr_dir_n, file_n)))

  def get_dir(self, path, e):
    if str(e) == '550 Failed to open file.':
      try:
        file_names = self.ftp_session.nlst()

        if not os.path.exists(path):
          os.makedirs(path)

        for file in file_names:
          self.get_file(os.path.join(path, file))

      except ftplib.error_perm as e:
        print(e)


def main():
  client = Client()


if __name__ == '__main__':
  main()