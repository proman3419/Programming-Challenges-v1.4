import requests
from bs4 import BeautifulSoup
from datetime import datetime
import os


class Downloader:
  def __init__(self):
    self.url = ''
    self.content = ''
    self.dir = ''
    self.images_dir = ''

    self.get_url()
    self.get_dirs_names()
    self.create_dirs()
    self.get_website()

  def get_url(self):
    while True:
      self.url = input('url: ')
      if 'boards.4channel.org' in self.url:
        print('Valid url')
        break
      else:
        print('Not a 4chan url')

  def get_dirs_names(self):
    temp = self.url.split('/')
    self.dir = '{}_{}_{}'.format(temp[3], 
                                 temp[5].split('#')[0],
                                 str(datetime.now()).replace(' ', '_').split('.')[0])
    self.images_dir = 'images'

  def create_dirs(self):
    print('Creating local directories')
    os.mkdir(self.dir)
    os.mkdir(os.path.join(self.dir, self.images_dir))

  def get_website(self):
    print('Downloading the webpage\'s code')
    response = requests.get(self.url)

    if str(response.status_code) == '200':
      print('Downloading of the webpage\'s code has succeeded')
      self.content = response.text
      self.scrape_website()
      self.save_website()
    else:
      print('The webpage doesn\'t exist or is unavaliable')

  def scrape_website(self):
    soup = BeautifulSoup(self.content, 'html.parser')
    self.get_body(soup)
    self.get_images(soup)

  def get_body(self, soup):
    thread = soup.findAll('div', {'class': 'thread'})[0]
    self.content = '<!DOCTYPE HTML><body>{}</body>'.format(thread)

  def get_images(self, soup):
    print('Downloading images')
    images = soup.findAll('img', href=False)
    i = j = 0

    for img in images:
      print('Downloading image {}/{}'.format(i, len(images)))
      img_f = os.path.join(self.images_dir, img.get('src').split('/')[-1])

      # Download image data
      response = requests.get('http:' + img.get('src'))
      if str(response.status_code) == '200':
        img_data = response.content
      
        # Save image
        with open(os.path.join(self.dir, img_f), 'wb') as f:
          f.write(img_data)

        # Replace image's source in html
        # src
        self.content = self.content.replace(img.get('src'), img_f)
        # href
        self.content = self.content.replace(img.get('src').replace('s', ''), img_f)        
        j += 1

      i += 1

    print('Downloading {}/{} images has succeeded'.format(j, len(images)))

  def save_website(self):
    file_path = os.path.join(self.dir, self.dir + '.html')

    with open(file_path, 'w') as f:
      f.write(self.content)

def main():
  downloader = Downloader()


if __name__ == '__main__':
  main()